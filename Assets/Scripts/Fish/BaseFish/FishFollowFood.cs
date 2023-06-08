using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.Serialization;

public class FishFollowFood : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector2 moveDirection;
    private int rotationToTheLeft;
    private int rotationToTheRight = 0;
    public bool haveFood;

    [SerializeField] private FishDriversManager fishDriversManager;

   // [SerializeField] private FishTypeSO fishTypeSO;
    // Update is called once per frame
   public void MoveToFood()
   {
       var food = FindClosestPieceOfFood(fishDriversManager.fishTypeSO.foodTag);
       
        if (food)
        {
            //get the distance between the fish and food
            float distance = Vector3.Distance(food.transform.position, transform.position);
            if (distance > 0f )
            {
                transform.position = Vector3.MoveTowards(transform.position, food.transform.position, moveSpeed/100f);
            }

            if (transform.position.x > food.transform.position.x) //when food is on the left
            {
                MakeFishRotateToTheLeft();
            }

            if (transform.position.x < food.transform.position.x) // when food is on the right
            {
                MakeFishRotateToTheRight(); 
            }
        }
       
        //this one will be use for the eye
        // if(target != null)
        // {
        //     transform.LookAt(target, Vector3.up);
        // }
    }

   public GameObject FindClosestPieceOfFood(string tag)
   {
       GameObject[] foods = GameObject.FindGameObjectsWithTag(tag);
       //if there is no foods found around then return nothing
       if(foods.Length == 0)
       {
           haveFood = false;
           return null;
       }

       GameObject closest;

       // If there is only exactly one anyway skip the rest and return it directly
       if(foods.Length == 1)
       {
           haveFood = true;
           closest = foods[0];
           return closest;
       }

       // Otherwise: Take the nearest food
       closest = foods.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();//put the closest food at the start of the list
       haveFood = true;
       return closest;
   }

   public void MakeFishRotateToTheLeft()
   {
       //select between 2 value ranomly
       if(Random.value<0.5f)
           rotationToTheLeft=180;
       else
           rotationToTheLeft=-180; 
            
       Vector3 direction = new Vector3(transform.rotation.eulerAngles.x, rotationToTheLeft, transform.rotation.eulerAngles.z);
       Quaternion targetRotation = Quaternion.Euler(direction);
       transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);
      // fishDriversManager.faceToTheRight = false;
   }

   public void MakeFishRotateToTheRight()
   {
       Vector3 direction = new Vector3(transform.rotation.eulerAngles.x, rotationToTheRight, transform.rotation.eulerAngles.z);
       Quaternion targetRotation = Quaternion.Euler(direction);
       transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);
      // fishDriversManager.faceToTheRight = true;
   }
}
