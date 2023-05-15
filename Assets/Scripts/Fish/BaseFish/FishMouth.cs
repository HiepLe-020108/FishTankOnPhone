using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//attach to fishmouth
public class FishMouth : MonoBehaviour
{
    [SerializeField] private FishDriversManager fishDriversManager;
    [SerializeField] private FishTypeSO fishTypeSO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(fishTypeSO.foodTag)) 
        {
            if (fishDriversManager.fishHungry)//only eat when the fish is hungry
            {
                fishDriversManager.FishGetFeed();
                Destroy(other.gameObject);
            }
        }
    }
}
