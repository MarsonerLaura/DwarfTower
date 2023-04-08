using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Threading.Tasks;

public class TowerShootFirst : MonoBehaviour
{
    [SerializeField]
    private TowerFindEnemy findEnemy;

    [SerializeField]
    private EnemyMovementManager enemyMovementManager;

    [SerializeField]
    private int _bulletsShot = 1;

    public int BulletShots
    {
        set { _bulletsShot = value; }
        get { return _bulletsShot; }
    }

    [SerializeField]
    private ProjectilePooling pool;
    

    [SerializeField]
    private float cooldown;

    public float Cooldown
    {
        set { cooldown = value; }
        get { return cooldown; }
    }
    

    private float countcooldown = 0;


    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks feedback;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbackSetActive setActiveFeed;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbackPosition positionFeed;
    

    [SerializeField]
    private int damage = 20;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // Update is called once per frame
    void Update()
    {
        countcooldown -= Time.deltaTime;
        if (countcooldown < 0)
        {
            countcooldown = cooldown;
            SpawnProjectile();
        }
    }


    private async void SpawnProjectile()
    {
        for (int i = 0; i < _bulletsShot; i++)
        {
            GameObject projectile = null;
            List<Transform> enemys = FindEnemyClosestToGoal();
            if (enemys.Count != 0)
            {
                do
                {
                    projectile = pool.GetProjectile();
                } while (projectile == null);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.Target = enemys;
                projectileScript.Damage = damage;
                projectileScript.SetDamageText();
                setActiveFeed.TargetGameObject = projectile;
                positionFeed.AnimatePositionTarget = projectile;
                feedback.PlayFeedbacks();
                await Task.Delay(System.TimeSpan.FromSeconds(feedback.TotalDuration));
                AfterFeedBack(projectile);


            }
        }
        
    }

    private List<Transform> FindEnemyClosestToGoal()
    {
        List<EnemyMovementManager.EnemyInformation> enemysInformations = enemyMovementManager.Enemies;
        List<GameObject> enemy = findEnemy.FindEnemies();
        List<Transform> ret = new List<Transform>();
        foreach (EnemyMovementManager.EnemyInformation enemie in enemysInformations)
        {
            if (enemy.Contains(enemie.enemieTransform.gameObject))
            {
                 ret.Add(enemie.enemieTransform.gameObject.transform);
            }
        }

        return ret;

    }

    public void AfterFeedBack(GameObject projectile)
    {
        if (projectile != null)
        {
            projectile.GetComponent<Projectile>().enabled = true;
            projectile = null;
            setActiveFeed.TargetGameObject = null;
            positionFeed.AnimatePositionTarget = null;
        }
    }

    
}
