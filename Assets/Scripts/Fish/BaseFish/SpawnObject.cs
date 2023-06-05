using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private FishTypeSO fishTypeSO;
    private float timeLeft;
    public bool timerOn;
    private GameObject spawnedObject;

    private void Start()
    {
        timerOn = true;
        timeLeft = fishTypeSO.timeNeedToSpawn;
        ShowCollectMoneyBoard.OnObjectDestroyed += OnObjectDestroyedHandler;
    }

    private void Update()
    {
        Debug.Log(timeLeft);
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = 0;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        if (fishTypeSO.objectToSpawn.GetComponent<Coin>() != null)
        {
            spawnedObject = Instantiate(fishTypeSO.objectToSpawn, transform.position, Quaternion.identity);
            timerOn = true;
            timeLeft = fishTypeSO.timeNeedToSpawn;
        }
        else if (fishTypeSO.objectToSpawn.GetComponent<ShowCollectMoneyBoard>() != null)
        {
            spawnedObject = Instantiate(fishTypeSO.objectToSpawn, transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            timerOn = false;
            
        }
        else
        {
            Debug.LogWarning("SpawnObject: Unknown object script.");
        }

        
    }

    private void OnObjectDestroyedHandler()
    {
        timerOn = true;
        timeLeft = fishTypeSO.timeNeedToSpawn;
    }

    private void OnDisable()
    {
        ShowCollectMoneyBoard.OnObjectDestroyed -= OnObjectDestroyedHandler;
    }
}