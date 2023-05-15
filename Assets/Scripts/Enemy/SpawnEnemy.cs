using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPlaces = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnEnemys = new List<GameObject>();
    
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;

    [SerializeField] private bool stopSpawn;

    public void StartSpawnFunction()
    {
        InvokeRepeating("SpawnEnemyFunction",spawnTime, spawnDelay );
    }
    private void SpawnEnemyFunction()
    {
        int i;
        int j;
        i = UnityEngine.Random.Range(0, spawnPlaces.Count);
        j = UnityEngine.Random.Range(0, spawnEnemys.Count);
        Instantiate(spawnEnemys[j], spawnPlaces[i].transform.position, spawnPlaces[i].transform.rotation);
        if (stopSpawn)
        {
            CancelInvoke("SpawnEnemyFunction");
        }
    }
}
