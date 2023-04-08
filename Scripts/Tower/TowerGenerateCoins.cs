using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerGenerateCoins : MonoBehaviour
{
    [SerializeField]
    private bool upgrade1 = false;
    [SerializeField]
    private bool upgrade2 = true;
    [SerializeField]
    private bool upgrade3 = false;

    [SerializeField]
    private float startCooldown = 20.0f;
    [SerializeField]
    private float currentCooldown = 20.0f;

    [SerializeField]
    private ProjectilePooling coinPool;
    private GameObject coin = null;

    [SerializeField]
    private SpriteRenderer circle;

    [SerializeField]
    private TowerFindEnemy findTowers;

    [SerializeField]
    private TowerFindEnemy findEnemies;
    List<GameObject> lastEnemiesInRange = new List<GameObject>();


    [SerializeField]
    private MoreMountains.TopDownEngine.CountCoins countCoins;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks feedbacks;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbackSetActive setActiveFeedback;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbackPosition positionFeedback;

    [SerializeField]
    private float enemyDeathCooldownReduce = 5.0f;

    [SerializeField]
    private float towerCooldownReduce = 1.5f;

    [SerializeField]
    private Vector3 randomPositionSize;

    [SerializeField]
    private Vector3 defaultPosition;

    [SerializeField]
    private float startSpawnCoinCooldown = 10.0f;

    [SerializeField]
    private float currentSpawnCoinCooldown = 10.0f;

    [SerializeField]
    private GameObject coinToSpawn;
    private GameObject currentCoin;

    private void Awake()
    {
        currentCooldown = startCooldown;
        currentSpawnCoinCooldown = startSpawnCoinCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (upgrade1)
        {
            ExecuteUpgrade1();
        }
        if (upgrade2)
        {
            ExecuteUpgrade2();
        }
        if (upgrade3)
        {
            ExecuteUpgrade3();
        }
        if ((upgrade2 || upgrade1)&& !circle.enabled){
            circle.enabled = true;
        }
        if (!upgrade1 && !upgrade2&&circle.enabled)
        {
            circle.enabled = false;
        }
        BasicTower();
    }

    /*
     * Spawns temporary Coins the Players can collect
     */
    private void ExecuteUpgrade3()
    {
        currentSpawnCoinCooldown -= Time.deltaTime;
        if (currentSpawnCoinCooldown <= 0)
        {
            SpawnCoin();
            currentSpawnCoinCooldown = startSpawnCoinCooldown;
        }
    }

    private void SpawnCoin()
    {
        float x = Random.Range(defaultPosition.x, defaultPosition.x + randomPositionSize.x);
        float y = Random.Range(defaultPosition.y, defaultPosition.y + randomPositionSize.y);
        float z = Random.Range(defaultPosition.z, defaultPosition.z + randomPositionSize.z);
        if (currentCoin == null)
        {
            currentCoin = coinToSpawn;
            currentCoin = Instantiate(coinToSpawn);
        }
        currentCoin.transform.position = new Vector3(x, y, z);
        if (!currentCoin.activeInHierarchy)
        {    
            currentCoin.SetActive(true);
            
        }
        currentCoin.GetComponentsInChildren<MoreMountains.Feedbacks.MMFeedbacks>()[1].Initialization();
        currentCoin.GetComponentsInChildren<MoreMountains.Feedbacks.MMFeedbacks>()[1].PlayFeedbacks();
        
    }

    /*
     * Speeds the cooldown of the coinspawn if there are towers in a radius of 5
     */
    private void ExecuteUpgrade2()
    {
        List<GameObject> towersInRange = findTowers.FindEnemies();
        foreach (GameObject tower in towersInRange)
        {
            currentCooldown -= towerCooldownReduce*Time.deltaTime;
        }
    }

    //TODO test
    /*
     * Speeds the cooldown of the coinspawn if Enemies in a radius of 5 die
     */
    private void ExecuteUpgrade1()
    {
        List<GameObject> currentEnemiesInRange = findEnemies.FindEnemies();
        foreach (GameObject enemy in lastEnemiesInRange)
        {
            if(!enemy.activeInHierarchy)
            {
                currentCooldown -= enemyDeathCooldownReduce;
            }
        }
        lastEnemiesInRange.Clear();
        foreach (GameObject enemy in currentEnemiesInRange)
        {
            lastEnemiesInRange.Add(enemy);
        }
    }

    public void BasicTower()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            ShootCoin();
            currentCooldown = startCooldown;
            
        }
    }

    /*
     * Shoots a coin upwards and plays the according Animations 
     */
    private void ShootCoin()
    {
        if (coin == null)
        {
            coin = coinPool.GetProjectile();
        }
        setActiveFeedback.TargetGameObject = coin;
        positionFeedback.AnimatePositionTarget = coin;
        feedbacks.PlayFeedbacks();
    }

    /*
     * Extra Coins if Enemies in a radius of 5 die
     */
    public void SelectUpgrade1()
    {
        upgrade1 = true;
    }

    /*
     * Extra Coins if there are towers in a radius of 5
     */
    public void SelectUpgrade2()
    {
        upgrade2 = true;
    }

    /*
     * Spawns temporary Coins the Players can collect
     */
    public void SelectUpgrade3()
    {
        upgrade3 = true;
    }

    /*
     * Removes the coin from the scene with effects and adds the coin value to the overall count
     */
    public void CleanScene()
    {
        
        if (coin != null)
        {
            coin.GetComponent<MoreMountains.TopDownEngine.Coin>().PickedMMFeedbacks.PlayFeedbacks();
            countCoins.IncreaseCoinCount(10);
            coin.SetActive(false);
            coin = null;
            setActiveFeedback.TargetGameObject = null;
            positionFeedback.AnimatePositionTarget = null;
        }
    }
}
