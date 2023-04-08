using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int poolSizeOfPool;

    private List<GameObject> poolOfObjects = new List<GameObject>();

    private void Awake()
    {
        for(int i=0; i<poolSizeOfPool; i++)
        {
            AddToPool(true);
        }
    }

   

    public GameObject GetObjectFromPool()
    {
        foreach(GameObject poolObject in poolOfObjects)
        {
            if (poolObject.activeInHierarchy)
            {
                return poolObject;
            }
        }
        AddToPool(false);
        return GetObjectFromPool();
    }

    private void AddToPool(bool isFromStartPool)
    {
        GameObject instantiatedEnemy = Instantiate(enemyPrefab, transform);
        instantiatedEnemy.SetActive(false);
        poolOfObjects.Add(instantiatedEnemy);
        if (!isFromStartPool)
        {
            poolSizeOfPool++;
        }
    }
}
