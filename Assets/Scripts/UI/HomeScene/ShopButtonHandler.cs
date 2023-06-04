using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class ShopButtonHandler : MonoBehaviour
{
    private MoneyManager moneyManager;

    private int TotalFish;
    private int selectedIndex;
    private Button lastClickedButton;
    private GameObject fishObjectForData;
    private string fishID;
    private bool isActive;
    private bool isButtonClicked;

    [Header("List", order = 1)]
    [SerializeField] private List<string> tagOfFishType1 = new List<string>();
    [SerializeField] private List<string> tagOfFishType2 = new List<string>();
    [SerializeField] private List<FishTypeSO> fishTypeSOList = new List<FishTypeSO>();
    [SerializeField] private List<SetupButtonBuyFish> _buttonsScript = new List<SetupButtonBuyFish>();
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private List<GameObject> fishGameObjectList = new List<GameObject>();

    [Header("UI component", order = 2)]
    [SerializeField] private Button buyFishButton;
    [SerializeField] private TMP_Text totalNumberOfFishText;

    public static event Action fishBought;
    public static event Action<string> getFishID;

    private void Start()
    {
        buyFishButton.interactable = false;
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            fishGameObjectList[index].SetActive(false);
            _buttonsScript[index].SetupButton(fishTypeSOList[index], () => ShowInfo(index));
        }
        buyFishButton.onClick.AddListener(() => AddFish(selectedIndex));
        StartCoroutine(DelayedCheckButtonInteraction(0.01f));
        StartCoroutine(DelayedCheckNumberOfFish(0.01f));
        StartCoroutine(DelayedCheckButtonUnlock(0.01f));
    }
    private void CheckButtonUnlock()
    {
        Debug.Log("call");
        for (int i = 0; i < fishGameObjectList.Count; i++)
        {
            GameObject fishObject = fishGameObjectList[i];
            bool isUnlocked = fishObject.GetComponent<FishIDMakingAndBought>().beenUnlock;
            Button button = _buttons[i];
            button.interactable = isUnlocked;
        }
    }

    private void ShowInfo(int index)
    {
        selectedIndex = index;
        buyFishButton.interactable = true;
        isButtonClicked = false;

        if (lastClickedButton != null)
        {
            lastClickedButton.interactable = true;
        }
        
        var clickedButton = _buttons[index].GetComponent<Button>();
        clickedButton.interactable = false;
        lastClickedButton = clickedButton;
    }

    private void AddFish(int index)
    {
        if (isButtonClicked)
            return;

        if (moneyManager.TotalMoney >= fishTypeSOList[index].FishPrice)
        {
            fishGameObjectList[index].SetActive(true);
            fishObjectForData = fishGameObjectList[index];
            fishID = fishObjectForData.GetComponent<FishIDMakingAndBought>().id;
            isActive = fishObjectForData.activeSelf;

            getFishID?.Invoke(fishID);

            Debug.Log("Fish ID: " + fishID);

            moneyManager.SubMoney(fishTypeSOList[index].FishPrice);

            // Tắt tính tương tác cho nút đã nhấn sau khi thêm cá
            if (lastClickedButton != null)
            {
                lastClickedButton.interactable = false;
                lastClickedButton = null;

                // Khởi động sự kiện fishBought với giá trị true
                fishBought?.Invoke();
            }

            isButtonClicked = true;
        }

        CheckNumberOfFish();
    }
    
    private void CheckNumberOfFish()
    {
        var fish1 = GameObject.FindGameObjectsWithTag(tagOfFishType1[0]).Length;
        var fish2 = GameObject.FindGameObjectsWithTag(tagOfFishType1[1]).Length;
        var fish3 = GameObject.FindGameObjectsWithTag(tagOfFishType2[0]).Length;
        var fishNum = fish1 + fish2 + fish3;
        totalNumberOfFishText.SetText(fishNum.ToString());
        TotalFish = fishNum;
    }

    private void CheckButtonInteraction()
    {
        for (int i = 0; i < fishGameObjectList.Count; i++)
        {
            GameObject obj = fishGameObjectList[i];
            bool isActive = obj.activeSelf;
            Button button = _buttons[i];
            button.interactable = !isActive;
        }
    }

    private IEnumerator DelayedCheckNumberOfFish(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckNumberOfFish();
    }

    private IEnumerator DelayedCheckButtonInteraction(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckButtonInteraction();
    }
    private IEnumerator DelayedCheckButtonUnlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckButtonUnlock();
    }
}
