using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public class EnemyInformation
    {
        public EnemyInformation(int pathnr, int currentPoint, Transform enemieTransform,float speed, float rotationSpeed,MoreMountains.TopDownEngine.Health enemieHealth)
        {
            this.pathnr = pathnr;
            this.currentPoint = currentPoint;
            this.enemieTransform = enemieTransform;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
            this.enemieHealth = enemieHealth;
        }

        public int pathnr;
        public int currentPoint;
        public Transform enemieTransform;
        public float speed;
        public float rotationSpeed;
        public float distanceToGoal;
        public MoreMountains.TopDownEngine.Health enemieHealth;
    }


    private List<EnemyInformation> enemies = new List<EnemyInformation>();

    [SerializeField]
    private MoreMountains.TopDownEngine.Health baseHealth;

    [SerializeField]
    private List<Transform> pathParents;

    private List<List<Transform>> paths = new List<List<Transform>>();

    [SerializeField]
    private List<float> pathLenghts = new List<float>();

    [SerializeField]
    private List<List<float>> lenghtFromPointToStart = new List<List<float>>();


    public List<EnemyInformation> Enemies
    {
        get { return enemies; }
    }


    private List<EnemyInformation> removeAfter = new List<EnemyInformation>();

    private void Awake()
    {
        foreach(Transform parent in pathParents)
        {
            float lenght = 0f;
            List<Transform> path = new List<Transform>();
            List<float> lenghtBetweenPointsPath = new List<float>();
            Transform lastChild = null;
            foreach (Transform child in parent)
            {
                path.Add(child);
                if (lastChild != null)
                {
                    float distanceBetwwenPoints = (lastChild.position - child.position).magnitude;
                    lenght += distanceBetwwenPoints;
                    lenghtBetweenPointsPath.Add(lenght);
                }
                else
                {
                    lenghtBetweenPointsPath.Add(0);
                }
                lastChild = child;
            }
            lenghtFromPointToStart.Add(lenghtBetweenPointsPath);
            paths.Add(path);
            pathLenghts.Add(lenght);
        }

       
    }





    private void FixedUpdate()
    {

        removeAfter.Clear();

        foreach (EnemyInformation enemie in enemies)
        {

            if (enemie.currentPoint < paths[enemie.pathnr].Count)
            {
                if (enemie.enemieHealth.CurrentHealth > 0)
                {
                    Vector3 direction = paths[enemie.pathnr][enemie.currentPoint].position - enemie.enemieTransform.position;
                    direction.y = 0;
                    Quaternion rotationTarget = Quaternion.LookRotation(direction);
                    enemie.enemieTransform.rotation = Quaternion.RotateTowards(enemie.enemieTransform.rotation, rotationTarget, enemie.rotationSpeed);

                    enemie.enemieTransform.Translate(Vector3.forward * enemie.speed, Space.Self);

                    enemie.distanceToGoal = DistanceToGoal(enemie);

                    if (direction.magnitude < 0.5f)
                    {
                        enemie.currentPoint++;
                    }
                }
                else
                {
                    removeAfter.Add(enemie);
                }
            }
            else
            {
                baseHealth.Damage(1, gameObject, 0, 0, Vector3.zero);
                enemie.enemieHealth.Damage(enemie.enemieHealth.CurrentHealth, gameObject, 0, 0, Vector3.zero);
                removeAfter.Add(enemie);
            }
        }

        foreach(EnemyInformation enemie in removeAfter)
        {

            enemies.Remove(enemie);
        }


        SortListByClosest();
    }

    private void SortListByClosest()
    {

        for(int i=0; i<enemies.Count; i++)
        {
            for(int j=i; j < enemies.Count; j++)
            {
                if(enemies[i].distanceToGoal > enemies[j].distanceToGoal)
                {
                    EnemyInformation speicher = enemies[i];
                    enemies[i] = enemies[j];
                    enemies[j] = speicher;
                }
            }
        }
        
    }


    private float DistanceToGoal(EnemyInformation enemyInformation)
    {
        int currentPoint = enemyInformation.currentPoint;
        int pathnr = enemyInformation.pathnr;
        if (paths[pathnr].Count > currentPoint)
        {
            Transform currentPos = enemyInformation.enemieTransform;
            float totalDistance = pathLenghts[pathnr];
            float distanceFromCurrentPointToGoal = totalDistance - lenghtFromPointToStart[pathnr][currentPoint];
            float distanceFromEnemieToCurrentPoint = (paths[pathnr][currentPoint].position - currentPos.position).magnitude;
            return distanceFromCurrentPointToGoal + distanceFromEnemieToCurrentPoint;
        }
        return 0;
    }

    public void AddEnemy(GameObject enemieModel, int pathNumber, float speed, float rotationSpeed, MoreMountains.TopDownEngine.Health enemieHealth)
    {
        enemies.Add(new EnemyInformation(pathNumber, 0, enemieModel.transform, speed, rotationSpeed, enemieHealth));
    }




}
