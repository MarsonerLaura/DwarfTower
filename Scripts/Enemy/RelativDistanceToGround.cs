using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativDistanceToGround : MonoBehaviour
{

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float _offset = 0f;

    private MeshRenderer meshRenderer;

    private float halfOfSize;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(meshRenderer != null)
        {
            halfOfSize = meshRenderer.bounds.extents.y;
        }
        else
        {
            Debug.LogWarning("Enemy does not have a MeshRenderer");
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+ Vector3.up, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 newPos = hit.point + Vector3.up*halfOfSize+Vector3.up * _offset;
           transform.position = newPos;
        }
    }
}
