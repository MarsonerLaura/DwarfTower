using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CoinBag
{

    private static int coins;

    private static UnityEvent coinsChanged = new UnityEvent();

    public static int Coins
    {
        get { return coins; }
    }

    public static void IncreaseCoinCount(int points)
    {
        coins += points;
        if(coinsChanged != null)
        {
            coinsChanged.Invoke();
        }
    }

    public static void Subscribe(UnityAction call)
    {
        coinsChanged.AddListener(call);
    }

    public static void DecreaseCoinCount(int points)
    {
        coins -= points;
        if (coinsChanged != null)
        {
            coinsChanged.Invoke();
        }
    }
}
