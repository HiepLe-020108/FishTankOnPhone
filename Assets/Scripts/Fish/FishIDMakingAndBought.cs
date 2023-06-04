using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIDMakingAndBought : MonoBehaviour, IDataPersistence
{
    public string id;
    public bool beenBought = false;
    public bool beenUnlock = false;

    [ContextMenu("Generate guid for id")]
    private void OnEnable() 
    {
        ShopButtonHandler.getFishID += OnGetFishID;
    }

    private void OnDisable() 
    {
        ShopButtonHandler.getFishID -= OnGetFishID;
    }

    private void OnGetFishID(string fishID)
    {
        if (fishID == id)
        {
            beenBought = true;
        }
    }
    public void LoadData(GameData data) 
    {
        data.fishHadBeenBuy.TryGetValue(id, out beenBought);
        this.gameObject.SetActive(beenBought);
        data.unlockedFish.TryGetValue(id, out beenUnlock);
        
    }

    public void SaveData(GameData data) 
    {
        if (data.fishHadBeenBuy.ContainsKey(id))
        {
            data.fishHadBeenBuy.Remove(id);
        }
        data.fishHadBeenBuy.Add(id, beenBought);
        
        
        if (data.unlockedFish.ContainsKey(id))
        {
            data.unlockedFish.Remove(id);
        }
        data.unlockedFish.Add(id, beenUnlock);
    }
}
