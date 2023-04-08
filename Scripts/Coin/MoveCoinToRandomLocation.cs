using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{

    public class MoveCoinToRandomLocation : Coin
    {
        [SerializeField]
        private Camera gameCamera;

        [SerializeField]
        private LayerMask rayCameraLayers;

        [SerializeField]
        private LayerMask rayAboveLayers;

        [SerializeField]
        private Vector3 randomPositionSize;

        [SerializeField]
        private Vector3 defaultPosition;

        [SerializeField]
        private LevelManager levelManager;

        

        protected override void Pick(GameObject picker)
        {
            base.Pick(picker);
            Vector3 posOfPicker = picker.transform.position;
            Vector3 posOfOtherPlayer = Vector3.zero; 
            string playerId = picker.MMGetComponentNoAlloc<Character>()?.PlayerID;
            foreach (Character character in levelManager.Players)
            {
                if(playerId != character.PlayerID)
                {
                    posOfOtherPlayer = character.gameObject.transform.position;
                }
            }
            Vector3 pos = Vector3.zero;
            RaycastHit hit;
            int dontCrash = 0;
            do
            {
                float x = Random.Range(defaultPosition.x-randomPositionSize.x, defaultPosition.x + randomPositionSize.x);
                float z = Random.Range(defaultPosition.z-randomPositionSize.z, defaultPosition.z + randomPositionSize.z);
                dontCrash++;
                if(Physics.Raycast(new Vector3(x,200,z), Vector3.down, out hit, Mathf.Infinity, rayCameraLayers))
                {
                    pos = hit.point;
                    transform.position = pos + Vector3.up;
                }
                else
                {
                    Debug.Log("SHit");
                }
            } while ( (!nothingAbove() || !canBeSeen()) && dontCrash<10000);
            if (dontCrash > 100)
            {
                Debug.Log("shit");
            }
        }


        private bool canBeSeen()
        {
            RaycastHit hit;
            Vector3 direction =   transform.position- gameCamera.transform.position;
            if (Physics.Raycast(gameCamera.transform.position,direction,out hit,Mathf.Infinity,rayAboveLayers,QueryTriggerInteraction.Collide))
            {
                Transform objectHit = hit.transform;
                
                if (objectHit == transform)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private bool nothingAbove()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position+(Vector3.up*100), Vector3.down, out hit, Mathf.Infinity, rayAboveLayers, QueryTriggerInteraction.Collide))
            {
                Transform objectHit = hit.transform;

                if (objectHit == transform)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(gameCamera.transform.position,transform.position);
        }
    }
}
