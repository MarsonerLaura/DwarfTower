using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuffAll : MonoBehaviour
{
    [SerializeField]
    private float speedPercentage = 0.3f;

    [SerializeField]
    private SpriteRenderer circle;

    [SerializeField]
    private TowerFindEnemy findPlayers;

    private class Leaver
    {
        public Leaver(GameObject leaver, float left)
        {
            this.leaver = leaver;
            this.timeLeft = left;
        }
        public GameObject leaver;
        public float timeLeft;
    }
    
    private List<GameObject> playersEntered = new List<GameObject>();
    
    private List<GameObject> playersInCircle = new List<GameObject>();
    
    private List<Leaver> playersLeft = new List<Leaver>();

    [SerializeField]
    private GameObject pickUpTowerObject;

    
    private float trackTimeInActiveAfterPlaced=0;



    void Update()
    {
      

        //todo fill lists with enemyinrange,....
        List<GameObject> players = findPlayers.FindEnemies();

        SetInAktiveIfPickedUP(players);

        //check if players entered or reentered the circle
        foreach (GameObject player in players)
        {
            int checkReentered = -1;
            for (int i = 0; i < playersLeft.Count; i++)
            {
                if (playersLeft[i].leaver.Equals(player))
                {
                    ChangePlayerSpeed(player, -speedPercentage);
                    playersEntered.Add(player);
                    checkReentered = i;
                }
            }
            if (checkReentered > -1)
            {
                playersLeft.RemoveAt(checkReentered);
            }
            if (!playersInCircle.Contains(player) && !playersEntered.Contains(player))
            {
                playersEntered.Add(player);
            }
           
            
        }
        //check if players left the circle
        foreach (GameObject player in playersInCircle)
        {
            if (!players.Contains(player))
            {
                playersLeft.Add(new Leaver(player,2));
            }
        }
        if (playersEntered.Count > 0)
        {
            HandleJustEnteredPlayers();
        }
        if (playersInCircle.Count > 0)
        {
            circle.enabled = true;
            HandlePlayersInCircle();
        }
        else
        {
            circle.enabled = false;
        }
        if (playersLeft.Count > 0)
        {
            HandleLeftPlayers();
        }
    }

    private void SetInAktiveIfPickedUP(List<GameObject> players)
    {
        if (!pickUpTowerObject.activeInHierarchy)
        {
            trackTimeInActiveAfterPlaced = pickUpTowerObject.GetComponent<MoreMountains.TopDownEngine.PickUpTower>().PlaceFeedBack.TotalDuration;
        }
        trackTimeInActiveAfterPlaced -= Time.deltaTime;

        if (trackTimeInActiveAfterPlaced > 0)
        {
            players.Clear();
        }
    }

    private void resetPlayerLeft(GameObject player)
    {
        throw new NotImplementedException();
    }

    private void HandleJustEnteredPlayers()
    {
        foreach (GameObject player in playersEntered)
        {
            ChangePlayerSpeed(player, speedPercentage);
            player.GetComponent<TowerFeedbacks>().PlaySpeedBuffTowerEnterFeedback();
            playersInCircle.Add(player);
        }
        playersEntered.Clear();
    }

    private void HandlePlayersInCircle()
    {
        foreach (GameObject player in playersInCircle)
        {
            player.GetComponent<TowerFeedbacks>().PlaySpeedBuffTowerActiveFeedback();
        }
    }

    private void HandleLeftPlayers()
    {
        float time = Time.deltaTime;
        List<Leaver> removePlayers = new List<Leaver>();
        for(int i = 0; i< playersLeft.Count; i++)
        {
 
            float timeLeft = playersLeft[i].timeLeft;

            playersInCircle.Remove(playersLeft[i].leaver);

            if (timeLeft < 0)
            {
                ChangePlayerSpeed(playersLeft[i].leaver, -speedPercentage);
                removePlayers.Add(playersLeft[i]);
                playersLeft[i].leaver.GetComponent<TowerFeedbacks>().StopSpeedBuffTowerParticles();
            }

            else if (timeLeft == 2)
            {
                playersLeft[i].leaver.GetComponent<TowerFeedbacks>().PlaySpeedBuffTowerLeaveFeedback();
            }
            playersLeft[i].timeLeft -= time;

        }
        foreach(Leaver leaver in removePlayers)
        {
            playersLeft.Remove(leaver);
        }
        

    }
    

    /*
     * newSpeed = oldSpeed + oldSpeed * speedIncrease
     * speedIncrease in % -> 30% = 0.3f
     */
    private void ChangePlayerSpeed(GameObject player, float speedIncrease)
    {
        float walkSpeed = player.GetComponent<MoreMountains.TopDownEngine.CharacterMovement>().WalkSpeed;
        player.GetComponent<MoreMountains.TopDownEngine.CharacterMovement>().MovementSpeed += walkSpeed * speedIncrease;
    }


}

