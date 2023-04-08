using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFeedbacks : MonoBehaviour
{
    /*
     * SpeedBuffTower
     */
    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks SpeedBuffTowerEnter;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks SpeedBuffTowerActive;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks SpeedBuffTowerLeave;

    [SerializeField]
    private ParticleSystem towerActiveParticles;

    [SerializeField]
    private AudioSource towerActiveSound;

    private float audioScourceBaseVolume;


    private void Awake()
    {
        audioScourceBaseVolume = towerActiveSound.volume;
    }

    public void PlaySpeedBuffTowerEnterFeedback()
    {
        SpeedBuffTowerEnter.PlayFeedbacks();
        towerActiveSound.Play();
        towerActiveSound.volume = audioScourceBaseVolume;
    }

    public void PlaySpeedBuffTowerActiveFeedback()
    {
        SpeedBuffTowerActive.PlayFeedbacks();
    }

    public void PlaySpeedBuffTowerLeaveFeedback()
    {
        SpeedBuffTowerLeave.PlayFeedbacks();
        towerActiveSound.volume *= 0.5f;
    }

    public void StopSpeedBuffTowerParticles()
    {
        towerActiveParticles.Stop();
        towerActiveSound.Stop();
    }

}
