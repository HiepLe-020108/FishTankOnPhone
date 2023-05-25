using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonHandler : MonoBehaviour
{
    [SerializeField] Vector3 placeToSpawn;

    private MoneyManager moneyManager;
    [SerializeField] private List<string> tagOfFishType1 = new List<string>();
    [SerializeField] private List<string> tagOfFishType2 = new List<string>();
    
    [SerializeField] private List<SetupButtonBuyFish> _buttons;
    [SerializeField] private List<FishTypeSO> fishTypeSOList = new List<FishTypeSO>();
    [SerializeField] private List<GameObject> fishGameObjectList = new List<GameObject>();
    [SerializeField] private Button buyFishButton;
    
    [SerializeField] private TMP_Text totalNumberOfFishText;
    
    private int TotalFish;
    private int selectedIndex;

    void Start()
    {
        
        buyFishButton.interactable = false;
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            fishGameObjectList[index].SetActive(false);
            _buttons[index].SetupButton(fishTypeSOList[index],() => ShowInfo(index));
        }
        buyFishButton.onClick.AddListener(() => AddFish(selectedIndex));
    } 
    
    void ShowInfo(int index)
    {
       selectedIndex = index;
       buyFishButton.interactable = true;
    }

    void AddFish(int index)
    {
        if (moneyManager.TotalMoney >= fishTypeSOList[index].FishPrice)
        {
            fishGameObjectList[index].SetActive(true);
            //Here
            moneyManager.SubMoney(fishTypeSOList[index].FishPrice);
        }
        var fish1 = GameObject.FindGameObjectsWithTag(tagOfFishType1[0]).Length;
        var fish2 = GameObject.FindGameObjectsWithTag(tagOfFishType1[1]).Length;
        var fish3 = GameObject.FindGameObjectsWithTag(tagOfFishType2[0]).Length;

        var fishNum = fish1 + fish2 + fish3;
        totalNumberOfFishText.SetText(fishNum.ToString());
        TotalFish = fishNum;
    }

    void AddFish(GameObject fish)
    {
        GameObject clone = Instantiate(fish, placeToSpawn, Quaternion.identity);
        
        var fish1 = GameObject.FindGameObjectsWithTag(tagOfFishType1[0]).Length;
        var fish2 = GameObject.FindGameObjectsWithTag(tagOfFishType1[1]).Length;
        var fish3 = GameObject.FindGameObjectsWithTag(tagOfFishType2[0]).Length;
        var fishNum = fish1 + fish2 + fish3;
        totalNumberOfFishText.SetText(fishNum.ToString());
        TotalFish = fishNum;
    }
}