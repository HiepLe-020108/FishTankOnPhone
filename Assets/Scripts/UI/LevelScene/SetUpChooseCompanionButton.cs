using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

//attach to the buttons of the UI

public class SetUpChooseCompanionButton : MonoBehaviour
{
    [SerializeField] private FishTypeSO fishTypeSO;
    
    [SerializeField] private TMP_Text nameText;//text inside the button
    [SerializeField] private Button button;//the button isself
    [SerializeField] private TMP_Text inforText;//text inside ObjectHoldForTextInformation gameObject
    public void SetupButton(GameObject type, UnityAction setButton)
    {
        nameText.text = type.name;
        inforText.text = fishTypeSO.FishInformation;
        button.onClick.AddListener(setButton);
    }
}
