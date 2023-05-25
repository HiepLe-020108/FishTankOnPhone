using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockSave : MonoBehaviour, IDataPersistence
{
    [SerializeField] private WinCondition wincondition;
    
    [SerializeField] private int levelNumberIfPlayerWin = 0;
    private int levelNumber = 0;//this value will change base on if player win or lose


    void Start()
    {
        wincondition.OnPlayerWin += ChangeNumberOfLevelComplete;
    }

    private void Update()
    {
        Debug.Log(levelNumber);
    }

    public void ChangeNumberOfLevelComplete()
    {
        levelNumber = levelNumberIfPlayerWin;
    }

    private void OnDestroy()
    {
        wincondition.OnPlayerWin -= ChangeNumberOfLevelComplete;
    }

    public void SaveData(GameData data)
    {
        data.completedLevels = levelNumber;
    }
    public void LoadData(GameData data) { }
}
