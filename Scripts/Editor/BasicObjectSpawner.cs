using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class BasicObjectSpawner : EditorWindow
{
    string objectName = "";
    int objectID = 1;
    int numberToSpawn = 1;
    GameObject objectToSpawn;
    Transform parent;
    float objectScaleMin;
    float objectScaleMax;
    float spawnRadius = 5f;
    Vector3 spawnPosition = Vector3.zero;

    AnimBool randomRotation;
    bool randomRotationAroundX = false;
    bool randomRotationAroundY = false;
    bool randomRotationAroundZ = false;

    AnimBool colisionCheck;
    float sphereRadius = 1f;
    int layerMaskForColisionCHeck=0;
    List<Object> lastSpawnedObjects = new List<Object>();
    

    [MenuItem("Tools/Basic Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BasicObjectSpawner));
    }

    private void OnEnable()
    {
        colisionCheck = new AnimBool(false);
        colisionCheck.valueChanged.AddListener(Repaint);

        randomRotation = new AnimBool(false);
        randomRotation.valueChanged.AddListener(Repaint);
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Base Name", objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        numberToSpawn = EditorGUILayout.IntField("Number of Object To Spawn", numberToSpawn);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        spawnPosition = EditorGUILayout.Vector3Field("Spawn Position", spawnPosition);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Min Scale "+ objectScaleMin);
        EditorGUILayout.MinMaxSlider(ref objectScaleMin, ref objectScaleMax, 0.2f,3f);
        EditorGUILayout.PrefixLabel("Max Scale " + objectScaleMax);
        EditorGUILayout.EndHorizontal();

        objectToSpawn = EditorGUILayout.ObjectField("Prefab To Spawn", objectToSpawn, typeof(GameObject),false) as GameObject;
        parent = EditorGUILayout.ObjectField("Parent Of The Object", parent, typeof(Transform), true) as Transform;
        randomRotation.target = EditorGUILayout.ToggleLeft("Random Rotation", randomRotation.target);

        if (EditorGUILayout.BeginFadeGroup(randomRotation.faded))
        {
            EditorGUI.indentLevel++;
            randomRotationAroundX = EditorGUILayout.ToggleLeft("Random Rotation around x", randomRotationAroundX);
            randomRotationAroundY = EditorGUILayout.ToggleLeft("Random Rotation around y", randomRotationAroundY);
            randomRotationAroundZ = EditorGUILayout.ToggleLeft("Random Rotation around z", randomRotationAroundZ);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.Space();
        colisionCheck.target = EditorGUILayout.ToggleLeft("Look for colision before Spawn", colisionCheck.target);

        if (EditorGUILayout.BeginFadeGroup(colisionCheck.faded))
        {
            EditorGUI.indentLevel++;
            sphereRadius = EditorGUILayout.FloatField("Sphere Size", sphereRadius);
            LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(layerMaskForColisionCHeck), InternalEditorUtility.layers);
            layerMaskForColisionCHeck = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.Space();
        EditorGUI.BeginDisabledGroup(objectToSpawn == null || objectName == string.Empty || parent == null);

        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }
        if (GUILayout.Button("Reposition last Spawned Objects"))
        {
            ResetPositionOfLastObjects();
        }

        EditorGUI.EndDisabledGroup();

        if(objectToSpawn == null)
        {
            EditorGUILayout.HelpBox("No Gameobject set in the 'Prefab to Spawn' box", MessageType.Warning);
        }
        if (objectName == string.Empty)
        {
            EditorGUILayout.HelpBox("No Object Name set in the 'Base Name' box", MessageType.Warning);
        }
        if (parent == null)
        {
            EditorGUILayout.HelpBox("No Parent set in the 'Parent Of The Object' box", MessageType.Warning);
        }

    }

    private void ResetPositionOfLastObjects()
    {
        if(lastSpawnedObjects.Count == 0)
        {
            ShowNotification(new GUIContent("No Spawned object in List"));
            return;
        }
        foreach(GameObject newObject in lastSpawnedObjects)
        {   
            if (colisionCheck.target)
            {
                newObject.transform.position = OnColisionWithOthersReposition();
            }
            else
            {
                newObject.transform.position = RandomSpawnPos();
            }
        }
    }


    private void SpawnObject()
    {
        if(objectToSpawn == null)
        {
            ShowNotification(new GUIContent("No object for Spawn"));
            return;
        }
        if(objectName == string.Empty)
        {
            ShowNotification(new GUIContent("No name for object"));
            return;
        }

        lastSpawnedObjects.Clear();

        for (int i = 0; i < numberToSpawn; i++)
        {

            Vector3 spawnPos = RandomSpawnPos();
            if (colisionCheck.target)
            {
                spawnPos = OnColisionWithOthersReposition();
            }
            GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity, parent);
            lastSpawnedObjects.Add(newObject);
            newObject.name = objectName + objectID;
            newObject.transform.localScale = Vector3.one * Random.Range(objectScaleMin,objectScaleMax);
            newObject.transform.localRotation = RandomRotation();
            objectID++;
        }
    }
    

    private Quaternion RandomRotation()
    {
        float xRotation = 0;
        float yRotation = 0;
        float zRotation = 0;
        if (randomRotationAroundX)
        {
            xRotation = Random.Range(0, 360);
        }
        if (randomRotationAroundY)
        {
            yRotation = Random.Range(0, 360);
        }
        if (randomRotationAroundZ)
        {
            zRotation = Random.Range(0, 360);
        }

        return Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    private Vector3 OnColisionWithOthersReposition()
    {
        RaycastHit[] hits;
        Vector3 ret = Vector3.zero;
        int maxTries = 10000;
        do
        {
            ret = RandomSpawnPos();
            hits = Physics.SphereCastAll(ret, sphereRadius, Vector3.up,Mathf.Infinity, layerMaskForColisionCHeck);
          

            maxTries--;
        } while (hits.Length > 0 && maxTries>0);
        if (maxTries < 0)
        {
            Debug.Log("SHit");
        }
        return ret;
    }

    private Vector3 RandomSpawnPos()
    {
        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y)+spawnPosition;
        return spawnPos;
    }
}
