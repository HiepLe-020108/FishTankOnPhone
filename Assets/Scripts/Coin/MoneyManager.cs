using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour, IDataPersistence
{
    public int TotalMoney;
    [SerializeField] private int startingMoney = 100;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private bool isHomeScene = false;
    
    public delegate void OnMoneyChangedDelegate(float newTotalMoney);
    public event OnMoneyChangedDelegate OnMoneyChanged;
    private void Awake()
    {
        if (!isHomeScene)
        {
            TotalMoney = startingMoney;
        }
        if(isHomeScene)
        {
            if (TotalMoney == 0)
            {
                TotalMoney = startingMoney;
            }
        }
    }

    public void AddMoney(int moneyToAdd)
    {
        TotalMoney += moneyToAdd;
        OnMoneyChanged?.Invoke(TotalMoney);
        Debug.Log("OnMoneyChanged event called with value: " + TotalMoney);
    }

    public void SubMoney(int moneyToSubtrack)
    {
        TotalMoney -= moneyToSubtrack;
        OnMoneyChanged?.Invoke(TotalMoney);
    }

    private void Update()
    {
        moneyText.SetText(TotalMoney.ToString());
    }
    public void LoadData(GameData data)
    {
        TotalMoney = data.moneyCount;
    }

    public void SaveData(GameData data)
    {
        data.moneyCount = TotalMoney;
    }
}
