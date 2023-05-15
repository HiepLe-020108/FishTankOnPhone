using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

//Attach to the FishManager object, take buttons and Fish ScriptableObejcts
//count number of fish with tag 
//combine some fish with different tags but of same type and print it out to the Pause menu

public class SpawnAndManagerFish : MonoBehaviour
{
    [SerializeField] Vector3 placeToSpawn;

    private MoneyManager moneyManager;
    [SerializeField] private List<string> tagOfFishType1 = new List<string>();
    [SerializeField] private List<string> tagOfFishType2 = new List<string>();
    
    [SerializeField] private List<SetupButtonBuyFish> _buttons;
    [SerializeField] private List<FishTypeSO> fishTypeSOList = new List<FishTypeSO>();
    
    [SerializeField] private TMP_Text totalNumberOfFishText;
    public int totalFish;
    void Start()
    {
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            _buttons[index].SetupButton(fishTypeSOList[index],() => AddFish(index));
        }
    } 
    void AddFish(int index)
    {
        if (moneyManager.TotalMoney >= fishTypeSOList[index].FishPrice)
        {
            GameObject clone = Instantiate(fishTypeSOList[index].TheFish, placeToSpawn, Quaternion.identity);
            moneyManager.SubMoney(fishTypeSOList[index].FishPrice);
        }
        var fish1 = GameObject.FindGameObjectsWithTag(tagOfFishType1[0]).Length;
        var fish2 = GameObject.FindGameObjectsWithTag(tagOfFishType1[1]).Length;
        var fish3 = GameObject.FindGameObjectsWithTag(tagOfFishType2[0]).Length;

        var fishNum = fish1 + fish2 + fish3;
        totalNumberOfFishText.SetText(totalFish.ToString());
        totalFish = fishNum;
        
    }
}
