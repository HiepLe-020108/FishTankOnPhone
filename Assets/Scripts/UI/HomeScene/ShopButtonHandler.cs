using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    private Button lastClickedButton;

    public static event Action<bool> InteractableStateChanged;

    private void Start()
    {
        buyFishButton.interactable = false;
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            fishGameObjectList[index].SetActive(false);
            _buttons[index].SetupButton(fishTypeSOList[index], () => ShowInfo(index));
        }
        buyFishButton.onClick.AddListener(() => AddFish(selectedIndex));
    }

    private void ShowInfo(int index)
    {
        selectedIndex = index;
        buyFishButton.interactable = true;

        if (lastClickedButton != null)
        {
            // Restore interaction for the previously clicked button
            lastClickedButton.interactable = true;

            // Raise the InteractableStateChanged event with true
            InteractableStateChanged?.Invoke(true);
        }

        // Disable interaction for the current clicked button
        var clickedButton = _buttons[index].GetComponent<Button>();
        clickedButton.interactable = false;

        // Update the last clicked button
        lastClickedButton = clickedButton;

        // Raise the InteractableStateChanged event with false
        InteractableStateChanged?.Invoke(false);
    }

    private void AddFish(int index)
    {
        if (moneyManager.TotalMoney >= fishTypeSOList[index].FishPrice)
        {
            fishGameObjectList[index].SetActive(true);
            moneyManager.SubMoney(fishTypeSOList[index].FishPrice);

            // Disable interaction for the clicked button after adding the fish
            if (lastClickedButton != null)
            {
                lastClickedButton.interactable = false;
                lastClickedButton = null;

                // Raise the InteractableStateChanged event with true
                InteractableStateChanged?.Invoke(true);
            }
        }

        var fish1 = GameObject.FindGameObjectsWithTag(tagOfFishType1[0]).Length;
        var fish2 = GameObject.FindGameObjectsWithTag(tagOfFishType1[1]).Length;
        var fish3 = GameObject.FindGameObjectsWithTag(tagOfFishType2[0]).Length;
        var fishNum = fish1 + fish2 + fish3;
        totalNumberOfFishText.SetText(fishNum.ToString());
        TotalFish = fishNum;
    }
}
