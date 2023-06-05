using System;
using System.Collections;
using System.Collections.Generic;
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
        Destroy(gameObject);
        moneyManager.AddMoney(coinValue);
        OnDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagObjectThatCollectMoney))
        {
            moneyManager.AddMoney(coinValue);
            Destroy(gameObject);
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        OnObjectDestroyed?.Invoke();
    }
}