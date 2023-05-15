using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float TotalMoney;
    [SerializeField] private float startingMoney = 100f;
    [SerializeField] private TMP_Text moneyText;
    
    public delegate void OnMoneyChangedDelegate(float newTotalMoney);
    public event OnMoneyChangedDelegate OnMoneyChanged;
    private void Start()
    {
        TotalMoney = startingMoney;
        
    }

    public void AddMoney(float moneyToAdd)
    {
        TotalMoney += moneyToAdd;
        OnMoneyChanged?.Invoke(TotalMoney);
        Debug.Log("OnMoneyChanged event called with value: " + TotalMoney);
    }

    public void SubMoney(float moneyToSubtrack)
    {
        TotalMoney -= moneyToSubtrack;
        OnMoneyChanged?.Invoke(TotalMoney);
    }

    private void Update()
    {
        moneyText.SetText(TotalMoney.ToString());
    }
}
