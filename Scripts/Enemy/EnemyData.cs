using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{

    public string enemyTag;

    public string enemyLayer;

    public string enemyName;

    public string enemyDescription;

    public Sprite enemyImage;

    public GameObject enemyModel;

    public RuntimeAnimatorController enemyAnimator;

    public Collider enemyCollider;

    public Mesh enemyMesh;

    public float enemyMovementSpeed;

    public int enemyHealth;

    public MoreMountains.TopDownEngine.Health enemyHealthScript;

    public EnemyMovementSubscriber enemyMovementSubscriberScript;

    
}
