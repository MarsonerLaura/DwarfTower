using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new Upgrade", menuName ="Upgrade/Create New")]
public class UpgradeObject : ScriptableObject
{
    [SerializeField]
    public string titel;
    [SerializeField]
    public string text;
    [SerializeField]
    public int cost;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public Mesh mesh;

    public virtual void Upgrade(GameObject tower)
    {
        CoinBag.DecreaseCoinCount(cost);
    }

}
