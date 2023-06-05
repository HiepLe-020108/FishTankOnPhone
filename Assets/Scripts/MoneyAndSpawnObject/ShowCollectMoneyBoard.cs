using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowCollectMoneyBoard : MonoBehaviour
{
    [SerializeField] private int coinValue = 3;
    [SerializeField] private float fallingSpeed;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private string tagObjectThatCollectMoney;

    public static event Action OnObjectDestroyed;

    private void Start()
    {
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    private void OnMouseDown()
    {
        moneyManager.AddMoney(coinValue);
        OnObjectDestroyed?.Invoke();
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 5f);
    }
}