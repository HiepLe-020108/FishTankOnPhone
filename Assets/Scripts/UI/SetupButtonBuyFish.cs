using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
//this script will attach to any button used to buy fish
public class SetupButtonBuyFish : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText; 
    [SerializeField] private Button button;

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text inforText;

    [SerializeField] private ShopObjectInfoPanel shopObjectInfoPanel;
    // Start is called before the first frame update
    public void SetupButton(FishTypeSO type, UnityAction setButton)
    {
        nameText.text = type.FishName;
        priceText.text = type.FishPrice.ToString();
        image.sprite = type.FishImage;
        inforText.text = type.fishInfo;
        button.onClick.AddListener(() => {
            shopObjectInfoPanel.UpdateDisplayText(type.fishInfo);
            setButton();
        });
    }
}
