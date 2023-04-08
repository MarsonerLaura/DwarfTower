using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MoreMountains.TopDownEngine
{
    public class PickUpTower : MonoBehaviour
{
        public class InformationOfTowerPickUp
        {
            public GameObject tower;
            public MoreMountains.Feedbacks.MMFeedbacks placeFeedBack;
        }

        [SerializeField]
        private GameObject tower;
        [SerializeField]
        private bool deaktivateTowerScriptWhenPickedUp = true;
        [SerializeField]
        private GameObject towerScripts;
        [SerializeField]
        private GameObject player1ToggelButton;
        [SerializeField]
        private GameObject player2ToggelButton;
        [SerializeField]
        private GameObject player1And2ToggelButton;
        [SerializeField]
        private LayerMask upgradeZone;

        public UnityEvent upgradeEventStart;

        [SerializeField]
        private MoreMountains.Feedbacks.MMFeedbacks placeFeedBack;

        public MoreMountains.Feedbacks.MMFeedbacks PlaceFeedBack
        {
            get { return placeFeedBack; }
        }

        [SerializeField]
        private MoreMountains.Feedbacks.MMFeedbacks pickUpFeedback;

        private InformationOfTowerPickUp information = new InformationOfTowerPickUp();


        private bool isReadyToBePickedUp = true;


        private static UnityEvent _towerGotPickedUp = new UnityEvent();

        public bool IsReadyToBePickedUp
        {
            get { return isReadyToBePickedUp; }
        }

        [SerializeField]
        private List<Character> characters = new List<Character>();
        private List<PlayerPickUpTower> charactersPickUpScript = new List<PlayerPickUpTower>();

        private void Awake()
        {
            information.tower = tower;
            information.placeFeedBack = placeFeedBack;
        }

        private void Update()
        {  
            if (tracktimeBeforeTaking<0 && characters.Count > 0)
            {
                bool firstIsPlayer1 = characters[0].PlayerID == "Player1" ? true : false;

                if (characters.Count > 1)
                {
                    if (Input.GetAxisRaw("Player2_Interact") > 0)
                    {
                        if (firstIsPlayer1)
                        {
                            GotPickedUp(charactersPickUpScript[1]);
                        }
                        else
                        {
                            GotPickedUp(charactersPickUpScript[0]);
                        }
                    }
                    if (Input.GetAxisRaw("Player1_Interact") > 0)
                    {
                        if (firstIsPlayer1)
                        {
                            GotPickedUp(charactersPickUpScript[0]);
                        }
                        else
                        {

                            GotPickedUp(charactersPickUpScript[1]);
                        }
                    }
                }
                else
                {
                    if(firstIsPlayer1)
                    {
                        if (Input.GetAxisRaw("Player1_Interact") > 0)
                        {

                            GotPickedUp(charactersPickUpScript[0]);
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Player2_Interact") > 0)
                        {
                            GotPickedUp(charactersPickUpScript[0]);
                        }
                    }
                }
            }
        }


        public virtual void GotPickedUp(float timeBeforeTaking)
        {
            if (deaktivateTowerScriptWhenPickedUp)
            {
                towerScripts.SetActive(false);
            }
            if(pickUpFeedback != null)
            {
                pickUpFeedback.PlayFeedbacks();
            }
            if(_towerGotPickedUp != null)
            {
                _towerGotPickedUp.Invoke();
            }
            DisableAllButtons();
            isReadyToBePickedUp = false;
            characters.Clear();
            charactersPickUpScript.Clear();
            gameObject.SetActive(false);
        }

        public InformationOfTowerPickUp GiveInformation()
        {
            return information;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Character character = other.gameObject.GetComponent<Character>();
                characters.Add(character);
                PlayerPickUpTower characterpickUp = other.gameObject.GetComponent<PlayerPickUpTower>();
                charactersPickUpScript.Add(characterpickUp);
                ToggelButtons();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Character character = other.gameObject.GetComponent<Character>();
                characters.Remove(character);
                PlayerPickUpTower characterpickUp = other.gameObject.GetComponent<PlayerPickUpTower>();
                charactersPickUpScript.Remove(characterpickUp);
                ToggelButtons();
            }
        }

        public void PlayerWantsToInteract(GameObject player)
        {
            if (player.CompareTag("Player"))
            {
                Character character = player.GetComponent<Character>();
                characters.Add(character);
                PlayerPickUpTower characterpickUp = player.GetComponent<PlayerPickUpTower>();
                charactersPickUpScript.Add(characterpickUp);
                ToggelButtons();
            }
        }

        public void PlayerLeavesToInteract(GameObject player)
        {
            if (player.CompareTag("Player"))
            {
                Character character = player.GetComponent<Character>();
                characters.Remove(character);
                PlayerPickUpTower characterpickUp = player.GetComponent<PlayerPickUpTower>();
                charactersPickUpScript.Remove(characterpickUp);
                ToggelButtons();
            }
        }

        public void SetIsReadyToBeTakenTrue()
        {
            ToggelButtons();
            isReadyToBePickedUp = true;
        }

        private void DisableAllButtons()
        {
            player1ToggelButton.SetActive(false);
            player2ToggelButton.SetActive(false);
            player1And2ToggelButton.SetActive(false);
        }

        private void ToggelButtons()
        {
            DisableAllButtons();
            if (characters.Count > 0 && isReadyToBePickedUp)
            {
                if(characters.Count > 1)
                {
                    player1And2ToggelButton.SetActive(true);
                }
                else
                {
                    if (characters[0].PlayerID == "Player1")
                    {
                        player1ToggelButton.SetActive(true);
                    }
                    else
                    {
                        player2ToggelButton.SetActive(true);
                    }
                }
            }
        }

        public void GotPlaced()
        {
            if(IsOnUpgradeZone() && upgradeEventStart != null)
            {
                upgradeEventStart.Invoke();
            }
        }


        private bool IsOnUpgradeZone()
        {
           RaycastHit[] hits = Physics.BoxCastAll(transform.parent.position, Vector3.one, Vector3.up, Quaternion.identity, Mathf.Infinity, upgradeZone);
            

            return hits.Length > 0;
        }

        public void SubscribeToGotPickedUp(UnityAction call)
        {
            _towerGotPickedUp.AddListener(call);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position,Vector3.one);
        }
    }
}
