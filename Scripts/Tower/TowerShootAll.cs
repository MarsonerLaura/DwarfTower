using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootAll : MonoBehaviour
{
    [SerializeField]
    private TowerFindEnemy findEnemy;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float cooldown;

    private float countcooldown=0;

   

    // Update is called once per frame
    void Update()
    {
        countcooldown -= Time.deltaTime;
        if (countcooldown < 0)
        {
            countcooldown = cooldown;
            DoDamage();
        }
    }


    private void DoDamage()
    {
        List<GameObject> enemies = findEnemy.FindEnemies();
        foreach(GameObject enemie in enemies)
        {
            enemie.GetComponent<MoreMountains.TopDownEngine.Health>().Damage(damage,gameObject,2f,0f,Vector3.zero);
        }
    }
}
