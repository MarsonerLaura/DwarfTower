using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSubscriber : MonoBehaviour
{
    [SerializeField]
    private EnemyMovementManager movementManager;

    public EnemyMovementManager MovementManager
    {
        set { movementManager = value; }
    }

    private MoreMountains.TopDownEngine.Health health;

    [SerializeField]
    private int pathnr;

    public int Pathnr
    {
        set { pathnr = value; }
    }

    [SerializeField]
    private float speed;

    public float Speed
    {
        set { speed = value; }
    }
    
    [SerializeField]
    private float rotationSpeed;

    private void Awake()
    {
        health = GetComponent<MoreMountains.TopDownEngine.Health>();
    }

    public void Subscribe()
    {
        movementManager.AddEnemy(gameObject, pathnr, speed, rotationSpeed,health);
    }    
}
