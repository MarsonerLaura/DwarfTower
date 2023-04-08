using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateEnemyPrefab : EditorWindow
{

    string enemyTag;

    string enemyLayer;

    string enemyName;

    string enemyDescription;

    Object enemyImage;

    Object enemyModel;

    Object enemyAnimator;

    Object enemyCollider;

    Object enemyMesh;

    float enemyMovementSpeed;

    int enemyHealth;

    Object enemyHealthScript;

    Object enemyMovementSubscriberScript;

    Object prefabRef;

    EnemyData enemyDataScript;

    //GUI variables
    bool groupEnabled;



    [MenuItem("Tools / Create Enemy Prefab")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(CreateEnemyPrefab));
    }

    private void OnGUI()
    {
  
        GUILayout.Label("Enemy Information", EditorStyles.boldLabel);
        enemyName = EditorGUILayout.TextField("Enemy Name", enemyName);
        enemyDescription = EditorGUILayout.TextField("Enemy Description", enemyDescription);

        GUILayout.Label("Enemy Visuals", EditorStyles.boldLabel);
        enemyImage = EditorGUILayout.ObjectField("Enemy Image", enemyImage, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        enemyModel = EditorGUILayout.ObjectField("Enemy Model", enemyModel, typeof(GameObject), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
     
        GUILayout.Label("Enemy Data", EditorStyles.boldLabel);
        enemyMovementSpeed = EditorGUILayout.FloatField("Enemy Movementspeed",enemyMovementSpeed);
        enemyHealth = EditorGUILayout.IntField("Enemy Health", enemyHealth);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        enemyTag = EditorGUILayout.TextField("Enemy Tag", enemyTag);
        enemyLayer = EditorGUILayout.TextField("Enemy Layer", enemyLayer);
        enemyAnimator = EditorGUILayout.ObjectField("Enemy Animator", enemyAnimator, typeof(RuntimeAnimatorController), false, GUILayout.Width(EditorGUIUtility.currentViewWidth - 5), GUILayout.Height(EditorGUIUtility.singleLineHeight)); ;
        EditorGUILayout.EndToggleGroup();

        EditorGUI.BeginDisabledGroup(true);
        enemyHealthScript = EditorGUILayout.ObjectField("Enemy Health Script", enemyHealthScript, typeof(MoreMountains.TopDownEngine.Health), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)); ;
        enemyMovementSubscriberScript = EditorGUILayout.ObjectField("Enemy Movement Script", enemyMovementSubscriberScript, typeof(EnemyMovementSubscriber), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)); ;
        enemyMesh = EditorGUILayout.ObjectField("Enemy Mesh", enemyMesh, typeof(Mesh), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        enemyCollider = EditorGUILayout.ObjectField("Enemy Collider", enemyCollider, typeof(Collider), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)); ;
        EditorGUI.EndDisabledGroup();
        

        GUI.backgroundColor = Color.red;
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();


        if (GUILayout.Button("Create Prefab", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //create gameobject from base prefab
            prefabRef = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Game/Prefabs/Enemy/Enemy.prefab"); //path of base prefab
            GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(prefabRef);
            enemyDataScript = instanceRoot.GetComponent<EnemyData>();

            //modify prefab variant
            enemyDataScript.enemyTag = enemyTag;
            enemyDataScript.enemyLayer = enemyLayer;
            enemyDataScript.enemyName = enemyName;
            enemyDataScript.enemyDescription = enemyDescription;
            enemyDataScript.enemyImage = (Sprite)enemyImage;
            enemyDataScript.enemyModel = (GameObject)enemyModel;
            enemyDataScript.enemyAnimator = (RuntimeAnimatorController)enemyAnimator;
            enemyDataScript.enemyMovementSpeed = enemyMovementSpeed;
            enemyDataScript.enemyHealth = enemyHealth;
            enemyDataScript.enemyHealthScript = enemyHealthScript as MoreMountains.TopDownEngine.Health;
            enemyDataScript.enemyMovementSubscriberScript = enemyMovementSubscriberScript as EnemyMovementSubscriber;
            ModifyPrefabVariant(instanceRoot, enemyDataScript);

            //save gameobject as prefab variant
            string path = "Assets/Game/Prefabs/Enemy/"+enemyName+".prefab";
            GameObject pVariant = PrefabUtility.SaveAsPrefabAsset(instanceRoot, path);

            //destroy gameobject
            GameObject.DestroyImmediate(instanceRoot);
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Insert\n default Values", GUILayout.Width(130), GUILayout.Height(50)))
        {
            setDefaultValues();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void ModifyPrefabVariant(GameObject instanceRoot, EnemyData enemyDataScript)
    {
        instanceRoot.tag = enemyDataScript.enemyTag;
        instanceRoot.layer = LayerMask.NameToLayer(enemyDataScript.enemyLayer);
        MoreMountains.TopDownEngine.Health h = instanceRoot.GetComponent<MoreMountains.TopDownEngine.Health>();
        h.InitialHealth = enemyDataScript.enemyHealth;
        h.MaximumHealth = enemyDataScript.enemyHealth;
        h.SetHealth(enemyDataScript.enemyHealth);
        instanceRoot.GetComponent<EnemyMovementSubscriber>().Speed = enemyDataScript.enemyMovementSpeed;     
        }

    private void setDefaultValues()
    {
        enemyTag = "Enemy";
        enemyLayer = "Enemies";
        enemyName = "";
        enemyDescription = "";
        enemyImage = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        enemyModel = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/PolygonDungeonRealms/Prefabs/Characters/Chr_Skeleton_01.prefab");
        enemyAnimator = AssetDatabase.LoadMainAssetAtPath("Assets/Game/Animations/Animator Controller.controller");
        enemyHealthScript = AssetDatabase.LoadMainAssetAtPath("Assets/TopDownEngine/Common/Scripts/Characters/Core/Health.cs");
        enemyMovementSubscriberScript = AssetDatabase.LoadMainAssetAtPath("Assets/Game/Scripts/Enemy/EnemyMovementSubscriber.cs");
        enemyMovementSpeed = 0.01f;
        enemyHealth = 100;

    }

}
