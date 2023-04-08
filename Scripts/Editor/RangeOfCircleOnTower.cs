using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TowerFindEnemy))]
public class RangeOfCircleOnTower : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TowerFindEnemy towerFindEnemy = (TowerFindEnemy)target;
        towerFindEnemy.ResizeCircel();
    }
}
