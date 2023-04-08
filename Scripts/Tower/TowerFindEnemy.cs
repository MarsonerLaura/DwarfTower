using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFindEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform circleRadius;
    
    [SerializeField]
    private float radius;

    public float Radius
    {
        set { radius = value; }
        get { return radius; }
    }

    [SerializeField]
    private LayerMask layermask;

    private void Awake()
    {
        ResizeCircel();
    }

    public void ResizeCircel()
    {
        circleRadius.localScale = new Vector3(radius * 2, radius * 2, 1);
    }

    public List<GameObject> FindEnemies()
    {
        List<GameObject> ret = new List<GameObject>();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,radius,Vector3.up,Mathf.Infinity,layermask);//(transform.position,halfExtend,Vector3.up,Quaternion.identity, Mathf.Infinity, layermask);
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.gameObject != null)
            {
                ret.Add(hit.collider.gameObject);
            }
        }
        return ret;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
