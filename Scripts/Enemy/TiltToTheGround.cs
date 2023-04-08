using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltToTheGround : MonoBehaviour
{

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float rotationSpeed =10f;
    
    [SerializeField]
    private float maxRotation = 30f;

    private float anKatete;
    [SerializeField]
    private Vector3 point1Ray = Vector3.forward;
    [SerializeField]
    private Vector3 point2Ray = Vector3.back;

    private void Start()
    {
        anKatete = (point1Ray - point2Ray).magnitude;
    }

    void Update()
    {
        Tilt();
    }


    private void Tilt()
    {
        float distance1 = RayCastAt(point1Ray);
        float distance2 = RayCastAt(point2Ray);
        float gegenKatete = distance1 - distance2;
        float rotation = Mathf.Atan(gegenKatete/anKatete);
        Mathf.Min(maxRotation, rotation);
        Mathf.Max(-maxRotation, rotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation*100, transform.rotation.y, transform.rotation.z), rotationSpeed);
    }


    private float RayCastAt(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position+transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.distance;
        }
        return -1;
    }

   
}
