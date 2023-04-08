using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{

    [System.Serializable]
    private class Branch
    {
        public List<UpgradeObject> upgradeObjects;
    }
    
    

    [SerializeField]
    private float padding = 50f;

    [SerializeField]
    private UpgradeObject _noMoreUpgrades;

    [SerializeField]
    private List<Branch> upgradeTree = new List<Branch>();

    private List<int> upgradeTracker = new List<int>();

    private List<GameObject> upgradeButtons = new List<GameObject>();

    private List<UpgradeObject> _currentUpgrades = new List<UpgradeObject>();

    private MoreMountains.TopDownEngine.PickUpTower pickUp;
    
    [SerializeField]
    private GameObject upgradeButton;
    

    private void Awake()
    {
        pickUp = GetComponent<MoreMountains.TopDownEngine.PickUpTower>();
        pickUp.upgradeEventStart.AddListener(UpgradeDisplay);
        pickUp.SubscribeToGotPickedUp(UpgradeDisable);
        CoinBag.Subscribe(ButtonSetInteract);

        foreach(Branch branch in upgradeTree)
        {
            upgradeTracker.Add(0);
        }

        for(int i=0; i<upgradeTree.Count; i++)
        {

            GameObject instantOfButton = Instantiate(upgradeButton);
            upgradeButtons.Add(instantOfButton);
        }
        UpgradeDisable();
    }

    public void UpgradeDisplay()
    {
        
        for (int i=0; i < upgradeTree.Count; i++)
        {
            upgradeButtons[i].SetActive(true);
            if (upgradeTree[i].upgradeObjects.Count > upgradeTracker[i])
            {
                SetVariablesOfButton(upgradeButtons[i], upgradeTree[i].upgradeObjects[upgradeTracker[i]], i);
            }
            else
            {
                SetVariablesOfButton(upgradeButtons[i], _noMoreUpgrades, i);
            }
        }
        ButtonSetInteract();
    }

    public void UpgradeDisable()
    {
        foreach(GameObject button in upgradeButtons)
        {
            button.SetActive(false);
        }
    }


    public void SetVariablesOfButton(GameObject button, UpgradeObject upgradeInfo, int number)
    {
        if (_currentUpgrades.Count > number)
        {
            _currentUpgrades[number] = upgradeInfo;
        }
        else
        {
            _currentUpgrades.Add(upgradeInfo);
        }

        RectTransform buttontransform = button.transform.GetChild(0).GetComponent<RectTransform>();
        Button uiButton = button.transform.GetChild(0).GetComponent<Button>();
        Image image = button.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        TextMeshProUGUI titel = button.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI text = button.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cost = button.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();

        uiButton.onClick.RemoveAllListeners();
        
        uiButton.onClick.AddListener(delegate { upgradeInfo.Upgrade(transform.parent.gameObject); });
        uiButton.onClick.AddListener(delegate { NewUpdate(number); });
        
        buttontransform.position = new Vector3((number - 1) * (buttontransform.sizeDelta.x + padding) + 960f, buttontransform.position.y, buttontransform.position.z);
        image.sprite = upgradeInfo.image;
        titel.text = upgradeInfo.titel;
        text.text = upgradeInfo.text;
        if(upgradeInfo.cost== -1)
        {
            cost.text = "Max X";
        }
        else
        {
            cost.text = upgradeInfo.cost + "X";
        }

    }


    private void NewUpdate(int branch)
    {
        upgradeTracker[branch]++;
        if(upgradeTree[branch].upgradeObjects.Count > upgradeTracker[branch])
        {
            SetVariablesOfButton(upgradeButtons[branch], upgradeTree[branch].upgradeObjects[upgradeTracker[branch]], branch);
        }
        else
        {
            SetVariablesOfButton(upgradeButtons[branch], _noMoreUpgrades, branch);
            ButtonSetInteract();
        }

    }

    private void ButtonSetInteract()
    {
        for(int i=0; i<_currentUpgrades.Count; i++)
        {
            Button uiButton = upgradeButtons[i].transform.GetChild(0).GetComponent<Button>();
            uiButton.interactable = CoinBag.Coins>= _currentUpgrades[i].cost && _currentUpgrades[i].cost!=-1;
            
        }
    }
    


}
