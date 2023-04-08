using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpTower : MonoBehaviour
{
    [SerializeField]
    private MoreMountains.TopDownEngine.Character character;

    [SerializeField]
    private Transform modelOfChar;

    [SerializeField]
    private Transform positionForTowerPickUp;
    
    [SerializeField]
    private Transform positionForTowerPlace;

    [SerializeField]
    private float timeBeforePlacing = 1f;

    [SerializeField]
    private float interactRadius;

    [SerializeField]
    private LayerMask layerMaskToInteract;

    [SerializeField]
    private LayerMask placesForTower;

    private float tracktimeBeforePlacing;



    private GameObject towerToInteract = null;

    public GameObject TowerToInteract
    {
        get { return towerToInteract; }
    }

    private Transform oldParentOfTower;

    private GameObject pickUpObject;

    private MoreMountains.TopDownEngine.PickUpTower currentPickUpScript;

    MoreMountains.Feedbacks.MMFeedbacks placeFeedbackOfTower;

    private bool isHoldingTheTower = false;

   

    private void Update()
    {
        //RayCast to find nearest interactObject
        if (!isHoldingTheTower && tracktimeBeforePlacing <0)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactRadius, Vector3.up, Mathf.Infinity, layerMaskToInteract);
            hits = SortListClosestFirst(hits);
            bool towerNotReady = true;
            for (int i = 0; i < hits.Length && towerNotReady; i++)
            {

                GameObject hitObject = hits[i].collider.gameObject;
                if (hitObject != null)
                {
                    if (hitObject.GetComponent<MoreMountains.TopDownEngine.PickUpTower>().IsReadyToBePickedUp)
                    {
                        towerNotReady = false;
                        if (pickUpObject != hitObject)
                        {
                            if (currentPickUpScript != null && currentPickUpScript.enabled)
                            {
                                currentPickUpScript.PlayerLeavesToInteract(gameObject);
                            }
                            pickUpObject = hitObject;
                            currentPickUpScript = pickUpObject.GetComponent<MoreMountains.TopDownEngine.PickUpTower>();
                            currentPickUpScript.PlayerWantsToInteract(gameObject);
                            MoreMountains.TopDownEngine.PickUpTower.InformationOfTowerPickUp information = currentPickUpScript.GiveInformation();
                            towerToInteract = information.tower;
                            placeFeedbackOfTower = information.placeFeedBack;
                        }
                    }
                }
            }
            if (hits.Length == 0 && !isHoldingTheTower)
            {
                NoIteract();
            }
        }


        tracktimeBeforePlacing -= Time.deltaTime;
        if(towerToInteract != null && Input.GetButtonDown(character.PlayerID+ "_Interact") && tracktimeBeforePlacing<0)
        {
            tracktimeBeforePlacing = timeBeforePlacing;

            if (isHoldingTheTower)
            {
                if (RightPlaceToPlaceTower())
                {

                    isHoldingTheTower = false;
                    PlaceTower();
                }
                else
                {
                    Debug.Log("Cant Place Here");
                }
            }
            else
            {
                isHoldingTheTower = true;
                TakeTower();
            }
        }
    }

    private bool RightPlaceToPlaceTower()
    {

        return (Physics.Raycast(positionForTowerPlace.position + Vector3.up * 100, Vector3.down, Mathf.Infinity, placesForTower));
    }

    private RaycastHit[] SortListClosestFirst(RaycastHit[] hits)
    {
        for(int i=0; i < hits.Length-1; i++)
        {
            float distanceI = Distance(hits[i].transform.position, transform.position);
            for (int j = i+1; j < hits.Length; j++)
            {
                float distanceJ = Distance(hits[j].transform.position, transform.position);
                if (distanceI > distanceJ)
                {
                    RaycastHit speicher = hits[i];
                    hits[i] = hits[j];
                    hits[j] = speicher;
                }
            }
        }

        return hits;
    }

    private float Distance(Vector3 point1, Vector3 point2)
    {
        return (point1 - point2).magnitude;
    }

    private void PlaceTower()
    {
        towerToInteract.transform.parent = oldParentOfTower;
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        RaycastHit hit;
        if(Physics.Raycast(positionForTowerPlace.position + Vector3.up * 100, Vector3.down,out hit, Mathf.Infinity, placesForTower))
        {
            towerToInteract.transform.position = hit.point + Vector3.up * meshRenderer.bounds.extents.y;
        }
        else
        {
            Debug.Log("Shity raycast");
            towerToInteract.transform.position = positionForTowerPlace.position;
        }
        towerToInteract.transform.localRotation = Quaternion.identity;
        pickUpObject.SetActive(true);
        placeFeedbackOfTower.PlayFeedbacks();
        currentPickUpScript.GotPlaced();
        NoIteract();
    }

    private void NoIteract()
    {
        if (currentPickUpScript != null)
        {
            currentPickUpScript.PlayerLeavesToInteract(gameObject);
        }
        currentPickUpScript = null;
        pickUpObject = null;
        towerToInteract = null;
        oldParentOfTower = null;
    }

      private void TakeTower()
      {
        oldParentOfTower = towerToInteract.transform.parent;
        towerToInteract.transform.parent = modelOfChar;
        towerToInteract.transform.localRotation = Quaternion.identity;
        towerToInteract.transform.position = positionForTowerPickUp.position;
        currentPickUpScript.GotPickedUp(timeBeforePlacing);
    }
      

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

}
