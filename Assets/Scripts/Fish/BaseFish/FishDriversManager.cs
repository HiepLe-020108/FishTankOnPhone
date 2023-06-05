using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


//attach to all fish
public class FishDriversManager : MonoBehaviour
{
    [Header("Reference", order = 1)]
    [SerializeField] private FishFollowFood fishFollowFood;
    [SerializeField] private FishDumbMoveAround fishDumbMoveAround;
    [SerializeField] private FishGrow fishGrow;
    [SerializeField] private SpawnObject spawnObjectClass;
    [FormerlySerializedAs("_changeColor")] [SerializeField] private FishShowItIsHungry showItIsHungry;
    [SerializeField] private FishTypeSO fishTypeSO;
    
    public bool fishHungry = false;

    [SerializeField] private float timeTillFishHungry;
    public bool faceToTheRight;
    [SerializeField] private float lefpTime = 1;
 
    // Start is called before the first frame update
    void Start()
    {
        timeTillFishHungry = fishTypeSO.FishStamina;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTheFishHungry();
        showItIsHungry.MakeBubbleThoughtAppear(!fishHungry,fishHungry);
        if (fishHungry)
        {
            fishFollowFood.FindClosestPieceOfFood(fishTypeSO.foodTag);
        }
        if (fishHungry && fishFollowFood.haveFood)
        {
            
            fishFollowFood.MoveToFood();
        }
        //the fish is not hungry
        else
        {
            fishDumbMoveAround.MoveNormalFunction();
        }
        RotateFish();
        CheckIfTheFishCanSpawnCoin(); 
    }

    //this function is call when the fish eat
    public void FishGetFeed()
    {
        timeTillFishHungry = fishTypeSO.FishStamina;
        fishHungry = false;
    }
    
    void CheckIfTheFishHungry()
    {
        if (!fishHungry)
        {
            if (timeTillFishHungry > 0)
            {
                timeTillFishHungry -= Time.deltaTime;
            }
            else
            {
                timeTillFishHungry = 0;
                fishHungry = true;
            }
        }
    }
    
    void RotateFish()
    {
        if (faceToTheRight)
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * lefpTime);
        else
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * lefpTime);
    }
    
    
    //this function will change bool value in SpawnCoin script 
    //if fish hungry then,the timer on spawn coin function won't run
    public void CheckIfTheFishCanSpawnCoin()
    {
        if (spawnObjectClass == null)
        {
            return;
        }
        if (fishHungry)
        {
            spawnObjectClass.timerOn = false;
        }
        else
        {
            spawnObjectClass.timerOn = true;
        }
    }
}
