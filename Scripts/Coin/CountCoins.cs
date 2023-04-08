using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using TMPro;

namespace MoreMountains.TopDownEngine
{

    public class CountCoins : MonoBehaviour,MMEventListener<TopDownEnginePointEvent>
    {
        [SerializeField]
        private TextMeshProUGUI text;
        

        private void Awake()
        {
            MMEventManager.AddListener(this);
        }

        private void Start()
        {

            CoinBag.Subscribe(UpdateText);
        }

        public void OnMMEvent(TopDownEnginePointEvent eventType)
        {
            IncreaseCoinCount(eventType.Points);
        }


        public void IncreaseCoinCount(int points)
        {
            CoinBag.IncreaseCoinCount(points);
            UpdateText();
        }


        public void DecreaseCoinCount(int points)
        {
            CoinBag.IncreaseCoinCount(points);
            UpdateText();
        }


        private void UpdateText()
        {
            if (text != null)
            {

                text.text = CoinBag.Coins + " C";
            }
        }


    }
}
