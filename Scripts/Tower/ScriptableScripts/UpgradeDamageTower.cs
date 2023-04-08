using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Upgrade", menuName = "Upgrade/Damage Tower")]
public class UpgradeDamageTower : UpgradeObject
{
    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private bool _newProjectile=false;

    [SerializeField]
    private int _damage=0;

    [SerializeField]
    private float _range=0;

    [SerializeField, Range(0, 1)]
    private float _attackspeed=1;

    [SerializeField]
    private int _extraBullet = 0;

    public override void Upgrade(GameObject tower)
    {
        base.Upgrade(tower);
        if (_newProjectile)
        {
            tower.GetComponentInChildren<ProjectilePooling>().ChangeProjectile(_projectile);
        }
        TowerShootFirst shootfirst = tower.GetComponentInChildren<TowerShootFirst>();
        shootfirst.Damage += _damage;
        shootfirst.BulletShots += _extraBullet;
        TowerFindEnemy towerfind = tower.GetComponentInChildren<TowerFindEnemy>();
        towerfind.Radius += _range;
        towerfind.ResizeCircel();
        tower.GetComponentInChildren<TowerShootFirst>().Cooldown *= _attackspeed;
    }
}
