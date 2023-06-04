using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockSave : MonoBehaviour, IDataPersistence
{
    [SerializeField] private WinCondition wincondition;
    
    [SerializeField] private int levelNumberIfPlayerWin = 0;
    [SerializeField] private int levelNumber = 0;//this value will change base on if player win or lose

    [SerializeField] private string fishIDToUnlockIfWin;


    void Start()
    {
        wincondition.OnPlayerWin += WhenPlayerWinLevel;
    }
    private void OnDestroy()
    {
        wincondition.OnPlayerWin -= WhenPlayerWinLevel;
    }

    public void WhenPlayerWinLevel()
    {
        levelNumber = levelNumberIfPlayerWin;
        
    }
    public void SaveData(GameData data)
    {
        if (data.completedLevels <= levelNumber)
        {
            Debug.Log("call 113");
            data.completedLevels = levelNumber;
            if (data.unlockedFish.ContainsKey(fishIDToUnlockIfWin))
            {
                data.unlockedFish.Remove(fishIDToUnlockIfWin);
            }
            data.unlockedFish.Add(fishIDToUnlockIfWin, true);
        }
        
    }
    public void LoadData(GameData data) { }
}
