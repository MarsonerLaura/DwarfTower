using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    private class EnemyToSpawn
    {
        public int enemyId;
        public float secondsUntilSpawn;
        public int spawnpointId;
    }

    [System.Serializable]
    private class Wave
    {
        public float secondsUntilStart;
        public List<EnemyToSpawn> enemiesToSpawn = new List<EnemyToSpawn>();
    }

    [SerializeField]
    private Transform parentOfEnemies;

    [SerializeField]
    private EnemyMovementManager movementManager;

    [SerializeField]
    private List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private List<Wave> waves = new List<Wave>();


    [SerializeField]
    float countdown;
    GameObject currentEnemyToSpawn;
    GameObject currentSpawnPoint;

    int nextEnemyToSpawnId;
    int currentWaveId;
    bool finished = false;

    private void Awake()
    {
        if (waves.Count > 0)
        {
            if (waves[0].enemiesToSpawn.Count > 0)
            {
                countdown = waves[0].secondsUntilStart + waves[0].enemiesToSpawn[0].secondsUntilSpawn;
                currentEnemyToSpawn = enemies[waves[0].enemiesToSpawn[0].enemyId];
                currentSpawnPoint = spawnPoints[waves[0].enemiesToSpawn[0].spawnpointId];
                nextEnemyToSpawnId = 1;
                currentWaveId = 0;
            }
            else
            {
                Debug.Log("List of enemies to spawn is empty!");
            }
        }
        else
        {
            Debug.Log("List of waves is empty!");
        }
    }

    void Update()
    {
        if (!finished)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                HandleWave();
            }
        }
    }

    private void HandleWave()
    {
        //if current wave has no more enemies, set next wave and reset enemyToSpawn
        if (nextEnemyToSpawnId >= waves[currentWaveId].enemiesToSpawn.Count)
        {
            if (currentWaveId + 1 >= waves.Count)
            {
                finished = true;
            }
            else
            {
                currentWaveId++;
                nextEnemyToSpawnId = 0;
                countdown = waves[currentWaveId].secondsUntilStart;
                SpawnEnemy();
            }
        }
        else
        {
            SpawnEnemy();
        }
    }

    /*
     * Spawns currentEnemyToSpawn at currentSpawnPoint
     * Sets countdown, currentEnemyToSpawn and currentSpawnPoint to next in enemiesToSpawn
     * Sets finished to true if the end of the list is reached
     */
    private void SpawnEnemy()
    {
        //Spawn Enemy at SpawnPoint
        currentEnemyToSpawn.transform.position = currentSpawnPoint.transform.position;
        GameObject instantiatedEnemie = Instantiate(currentEnemyToSpawn,parentOfEnemies);

        //Move Enemy
        EnemyMovementSubscriber instantsOfMovement = instantiatedEnemie.GetComponent<EnemyMovementSubscriber>();
        instantsOfMovement.Pathnr = spawnPoints.IndexOf(currentSpawnPoint);
        instantsOfMovement.MovementManager = movementManager;
        instantsOfMovement.Subscribe();

        //check if endOfList is reached 
        if (nextEnemyToSpawnId >= waves[currentWaveId].enemiesToSpawn.Count)
        {
            Debug.Log("No enemies in this wave.");
        }
        //else update variables
        else
        {
            EnemyToSpawn nextEnemy = waves[currentWaveId].enemiesToSpawn[nextEnemyToSpawnId];
            countdown += nextEnemy.secondsUntilSpawn;
            if (enemies.Count > nextEnemy.enemyId)
            {
                currentEnemyToSpawn = enemies[nextEnemy.enemyId];
            }
            else
            {
                Debug.Log("Id of next enemy to spawn greater than the size of the list of enemies.");
            }
            if (spawnPoints.Count > nextEnemy.spawnpointId)
            {
                currentSpawnPoint = spawnPoints[nextEnemy.spawnpointId];
            }
            else
            {
                Debug.Log("Id of next spawnPoint greater than the size of the list of spawnPoints.");
            }
            nextEnemyToSpawnId++;
        }
    }
}
