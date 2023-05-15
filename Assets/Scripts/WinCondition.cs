using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    [SerializeField] private GameObject fishManager;
    [SerializeField] private MoneyManager moneyManager;
    private SpawnAndManagerFish fishScript;
    
    public int fishWinCondition = 10;
    
    [SerializeField] private int coinWinCondition = 100;
    [SerializeField] private GameObject doneMenu;

    // Start is called before the first frame update
    void Start()
    {
        fishScript = fishManager.GetComponent<SpawnAndManagerFish>();
        moneyManager.OnMoneyChanged += CheckIfPlayerWin;
    }
    
    public void CheckIfPlayerWin(float newTotalMoney)
    {
        Debug.Log(newTotalMoney);
        if (/*fishScript.totalFish >= fishWinCondition && */newTotalMoney >= coinWinCondition)
            {
                doneMenu.SetActive(true);
                Time.timeScale = 0f;
            }
    }

    void OnDestroy()
    {
        // destroy event listener
        moneyManager.OnMoneyChanged -= CheckIfPlayerWin;
    }
}
