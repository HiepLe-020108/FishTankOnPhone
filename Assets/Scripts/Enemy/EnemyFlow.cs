using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.Serialization;

public class EnemyFlow : MonoBehaviour
{
    [SerializeField]
    private GameObject top;[SerializeField]
    private GameObject bottom;[SerializeField]
    private GameObject right;[SerializeField]
    private GameObject left;
    
    [SerializeField] private float moveSpeed = 3f;
    
    private int rotationToTheLeft;
    private int rotationToTheRight = 0;
    [SerializeField]private bool haveFood;

    [SerializeField] private string targetTag;
    
    private float latestDirectionChangeTime;
    private float directionChangeTime; // the amount of time the fist stay in one direction
   
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    
    [SerializeField] private float fishDumbVelocity = 2f;
    [SerializeField] private float maxTime; //the max amount of time the fish can use to spent in one direction
    [SerializeField] private float minTime;

    [SerializeField] private FishDumbMoveAround fishDumbMoveAround;

    private bool faceToRight;

    private void Start()
    {
        top = GameObject.Find("Top"); //NAME
        bottom = GameObject.Find("Bottom");
        right = GameObject.Find("Right");
        left = GameObject.Find("Left");
    }

    private void Update()
    {
        if (haveFood)
        {
            MoveToTarget();
        }
        else
        {
            MoveNormalFunction();     
        }
    }

     void MoveToTarget()  
   {
       var target = FindClosestTarget(targetTag);
       
        if (target)
        {
            //get the distance between the fish and food
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance > 0f )
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed/100f);
            }

            if (transform.position.x > target.transform.position.x) //when food is on the left
            {
                RotateFish(faceToRight);
            }

            if (transform.position.x < target.transform.position.x) // when food is on the right
            {
                RotateFish(faceToRight);
            }
        }
   }

    GameObject FindClosestTarget(string tag)
   {
       GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
       //if there is no foods found around then return nothing
       if(objects.Length == 0)
       {
           haveFood = false;
           return null;
       }

       GameObject closest;

       // If there is only exactly one anyway skip the rest and return it directly
       if(objects.Length == 1)
       {
           haveFood = true;
           closest = objects[0];
           return closest;
       }

       // Otherwise: Take the nearest food
       closest = objects.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();//put the closest food at the start of the list
       haveFood = true;
       return closest;
   }

    void RotateFish(bool faceRight)
    {
        if (faceRight)
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime );
        else
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime);
    }
   
    void MoveNormalFunction()
    {
        //move fish 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        
        //if the directionChangeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            
            fishDumbMoveAround.CalculateNewMovementVector(out movementDirection, out movementPerSecond, out faceToRight);
        }
        //prevent fish from get out of tank at the top
        else if (transform.position.y > top.transform.position.y)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            CalculateNewMovementVectorWhenFishHitTheTop();
        }
        //prevent fish from get out of tank at the bottom
        else if ( transform.position.y < bottom.transform.position.y)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            CalculateNewMovementVectorWhenFishHitTheBottom();
        }
        //prevent fish from get out of tank at the right
        else if ( transform.position.x > right.transform.position.x)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            CalculateNewMovementVectorWhenFishHitTheRight();
        }
        //prevent fish from get out of tank at the left
        else if (transform.position.x < left.transform.position.x)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            CalculateNewMovementVectorWhenFishHitTheLeft();
        }
    }
   

    void CalculateNewMovementVectorWhenFishHitTheLeft()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        RotateFish(faceToRight);
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        
    }
    void CalculateNewMovementVectorWhenFishHitTheRight()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f)).normalized;
        RotateFish(faceToRight);
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        
    }
    void CalculateNewMovementVectorWhenFishHitTheTop()
    {
        //create a random direction but not  to the top vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f)).normalized;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        if (movementDirection.x < 0) //the fish is move to the left
        {
            RotateFish(faceToRight);
        }

        if (movementDirection.x > 0)//the fish is move to the right
        {
            RotateFish(faceToRight);
        }
    }
    void CalculateNewMovementVectorWhenFishHitTheBottom()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        if (movementDirection.x < 0) //the fish is move to the left
        {
            RotateFish(faceToRight);
        }

        if (movementDirection.x > 0)//the fish is move to the right
        {
            RotateFish(faceToRight);
        }
    }
}
