using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private FishTypeSO fishTypeSO;
    private float timeLeft;
    private bool alreadyRun;
    public bool timerOn;
    private GameObject spawnedObject;
    [SerializeField] private float spawnOffsetX = 0f;
    [SerializeField] private float spawnOffsetY = 0f;
    [SerializeField] private float spawnOffsetZ = 0f;

    private void Start()
    {
        alreadyRun = false;
        timerOn = true;
        timeLeft = fishTypeSO.timeNeedToSpawn;
    }
    
    public void RunTimer()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                Debug.Log($"Timer: {timeLeft}");
                timeLeft -= Time.deltaTime;
            }
            else
            {
                alreadyRun = true;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        Debug.Log($"Spawn fired");
        if (fishTypeSO.objectToSpawn.GetComponent<Coin>() != null)
        {
            Debug.Log($"Spawn coin");
            spawnedObject = Instantiate(fishTypeSO.objectToSpawn, transform.position, Quaternion.identity);
            timerOn = true;
            timeLeft = fishTypeSO.timeNeedToSpawn;
        }
        else if (fishTypeSO.objectToSpawn.GetComponent<ShowCollectMoneyBoard>() != null)
        {
            Debug.Log($"Spawn board");
            
            spawnedObject = Instantiate(fishTypeSO.objectToSpawn, transform);
            spawnedObject.transform.localPosition =
                new Vector3(this.transform.localPosition.x +spawnOffsetX,
                    this.transform.localPosition.y + spawnOffsetY, this.transform.localPosition.z);
            timerOn = false;
        }
        else
        {
            Debug.LogWarning("SpawnObject: Unknown object script.");
        }


    }
    private void OnObjectDestroyedHandler()
    {
        Debug.Log("call");
        timerOn = true;
        alreadyRun = false;
        timeLeft = fishTypeSO.timeNeedToSpawn;
    }

    private void OnEnable()
    {
        ShowCollectMoneyBoard.OnObjectDestroyed += OnObjectDestroyedHandler;
    }

    public void OnDisable()
    {
        ShowCollectMoneyBoard.OnObjectDestroyed -= OnObjectDestroyedHandler;
    }
}