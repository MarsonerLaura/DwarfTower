using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float rotationSpeed = 3f;

    [SerializeField]
    private float distanceBeforeExplode;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks feedback;


    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbackFloatingText textFeedback;


    private List<Transform> target = new List<Transform>();

    private int currentTarget = 0;

    public List<Transform> Target
    {
        get { return target; }
        set { target = value; }
    }

    private void Awake()
    {
        SetDamageText();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed,Space.Self);
        while (target[currentTarget] == null)
        {
            currentTarget++;
            if (currentTarget >= target.Count)
            {
                gameObject.SetActive(false);
            }
        }
        Quaternion rotationTarget = Quaternion.LookRotation(target[0].position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotationTarget,rotationSpeed);
    }

    private void Update()
    {
        if (!feedback.IsPlaying)
        {
            if ((target[currentTarget].position - transform.position).magnitude < distanceBeforeExplode)
            {
                DoDamage();
            }
        }        
    }

    public void SetDamageText()
    {
        textFeedback.Value = damage + "";
    }


    private void DoDamage()
    {
        feedback.PlayFeedbacks();
        target[currentTarget].gameObject.GetComponent<MoreMountains.TopDownEngine.Health>().Damage(damage, gameObject, 2f, 0f, Vector3.zero);
    }

   
}
