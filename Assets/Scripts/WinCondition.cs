using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour, IDataPersistence
{

    [SerializeField] private GameObject fishManager;
    [SerializeField] private MoneyManager moneyManager;
    
    private SpawnAndManagerFish fishScript;
    
    public int fishWinCondition = 10;
    
    [SerializeField] private int coinWinCondition = 100;
    [SerializeField] private GameObject doneMenu;
    [SerializeField] private int levelNumber = 0; //this number will equal with the level and set the number of complete level equal to this

    private bool isWin = false;
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
    //set how much level have been complete
    public void SaveData(GameData data)
    {
        Debug.Log("call");
        if (isWin)
        {
            Debug.Log("Is Win");
            data.completedLevels = levelNumber;
        }
    }
    public void LoadData(GameData data){}
}
