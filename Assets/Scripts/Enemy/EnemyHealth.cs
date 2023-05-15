using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to the Main body of the enemy, the object that player will tap
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private GameObject theEnemy;

    private void OnMouseDown()
    {
        health--;
        if(health ==0)
            Destroy(theEnemy);
    }
}
