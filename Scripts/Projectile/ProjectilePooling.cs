using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;


    public GameObject Projectile
    {
        get { return projectile; }
    }

    [SerializeField]
    private List<GameObject> pool = new List<GameObject>();

    [SerializeField]
    private int sizeOfPool = 100;

    [SerializeField]
    private int lastObject = 0;

    void Start()
    {
        for(int i=0; i < sizeOfPool; i++)
        {
            AddProjectileToPool(i);
        }
    }

    public GameObject GetProjectile()
    {
        lastObject = (lastObject + 1) % sizeOfPool;
        for(int i=0; i < pool.Count; i++)
        {
            if (!pool[(i+lastObject)%sizeOfPool].activeInHierarchy)
            {
                return pool[(i+lastObject)%sizeOfPool];
            }
        }
        AddProjectileToPool(sizeOfPool);
        sizeOfPool++;
        return GetProjectile();
    }

    public void ChangeProjectile(GameObject newProjectile)
    {
        projectile = newProjectile;
        pool.Clear();
        for (int i = 0; i < sizeOfPool; i++)
        {
            AddProjectileToPool(i);
        }

    }

    public void AddProjectileToPool(int i)
    {
        GameObject instant = Instantiate(projectile);
        instant.name = "Projectile" + i;
        instant.SetActive(false);
        pool.Add(instant);
    }
}
