using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    [SerializeField] private GameObject fishManager;
    [SerializeField] private MoneyManagerForLevel moneyManager;
    
    private SpawnAndManagerFish fishScript;
    
    public int fishWinCondition = 10;
    
    [SerializeField] private int coinWinCondition = 100;
    [SerializeField] private GameObject doneMenu;
    private bool isWin = false;

    public delegate void OnLevelWinDelegate();

    public event OnLevelWinDelegate OnPlayerWin;
    // Start is called before the first frame update
    void Start()
    {
        fishScript = fishManager.GetComponent<SpawnAndManagerFish>();
        moneyManager.OnMoneyChanged += CheckIfPlayerWin;
    }
    
    public void CheckIfPlayerWin(float newTotalMoney)
    {
        if (/*fishScript.totalFish >= fishWinCondition && */newTotalMoney >= coinWinCondition)
        {
            isWin = true;
            OnPlayerWin?.Invoke();
            doneMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnDestroy()
    {
        // destroy event listener
        isWin = false;
        moneyManager.OnMoneyChanged -= CheckIfPlayerWin;
    }
}
