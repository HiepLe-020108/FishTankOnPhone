using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int moneyCount;
    public int completedLevels;
    public SerializableDictionary<string, bool> unlockedFish;
    public SerializableDictionary<string, bool> buttonInteractableStates;
    public SerializableDictionary<string, bool> fishObjectsCollected;

    public GameData()
    {
        moneyCount = 0;
        completedLevels = 0;
        unlockedFish = new SerializableDictionary<string, bool>();
        buttonInteractableStates = new SerializableDictionary<string, bool>();
        fishObjectsCollected = new SerializableDictionary<string, bool>();
    }
}