using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopObjectInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText; 
    void Start()
    {
        displayText.text = "Info display place";
    }

    public void UpdateDisplayText(string text)
    {
        displayText.text = text;
    }
}
