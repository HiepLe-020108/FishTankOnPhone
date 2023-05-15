using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//attach to Fish
public class SpawnObject : MonoBehaviour
{
    [SerializeField] private FishTypeSO fishTypeSO;
    private float TimeLeft;
    public bool TimerOn; // if this var is true then timer of the SpawnCoin action will reduce till timer = 0, 
    //if this var is false then the fish will not be able to spawn object
    // this var will be used in FishDriverManager script which is attach to the Fish gameObject

    
    private void Start()
    {
        TimerOn = true;
        TimeLeft = fishTypeSO.timeNeedToSpawn;
    }

    private void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        GameObject cloneCoin = Instantiate(fishTypeSO.objectToSpawn, transform.position, Quaternion.identity);
        TimeLeft = fishTypeSO.timeNeedToSpawn;
        TimerOn = true;
    }
    
    
}
