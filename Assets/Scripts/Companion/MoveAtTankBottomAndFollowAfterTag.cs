using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveAtTankBottomAndFollowAfterTag : MonoBehaviour
{
    [Header("Variable", order = 2)]
    private GameObject bottom;
    private GameObject left;
    private GameObject right;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float latestDirectionChangeTime;
    private float directionChangeTime; // the amount of time the fist stay in one direction
    [SerializeField] private float maxTime; //the max amount of time the fish can use to spent in one direction
    [SerializeField] private float minTime; //the minimum amount of time the fish can use to spent in one direction
    [SerializeField] private string tagOfObjectFollowed;
    [SerializeField] private float moveSpeed;
    
    [Header("Reference", order = 1)]
    [SerializeField] private FishFollowFood fishFollowFood;

    private bool faceToTheRight;
    
    // Start is called before the first frame update
    void Start()
    {
        bottom = GameObject.Find("Bottom");
        right = GameObject.Find("Right");
        left = GameObject.Find("Left");
        CalcuateNewMovementVector();
        //if object is not at the bottom then move it to bottom
        if (transform.position.y > bottom.transform.position.y) //fall when the food is above the bottom of the tank
        {
            GetComponent<Transform>().position = new Vector3(Random.Range(left.transform.position.x,right.transform.position.x)
                , bottom.transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if there are any object to follow and if yes follow it
            var objectToFollow = fishFollowFood.FindClosestPieceOfFood(tagOfObjectFollowed);
            if (objectToFollow)
            {
                if (objectToFollow.transform.position.x != transform.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(objectToFollow.transform.position.x, bottom.transform.position.y, 0f),
                        moveSpeed/100f );
                }
            }
            else
            {
                MoveFish();
            }
        RotateThis();
    }

    
    void MoveFish()
    {
        //move fish 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            bottom.transform.position.y + (movementPerSecond.y * Time.deltaTime));
        
        //if the directionChangeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            //make the fish change direction after a random amount of time 
            directionChangeTime = Random.Range(minTime, maxTime);
            latestDirectionChangeTime = Time.time;
            CalcuateNewMovementVector();
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
    void CalcuateNewMovementVector(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0f).normalized;
        movementPerSecond = movementDirection * Random.Range(0f, 3f);
        if(movementDirection.x > 0f)
            faceToTheRight = true;
        if(movementDirection.x < 0f)
            faceToTheRight = false;
    }
    void CalculateNewMovementVectorWhenFishHitTheLeft()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * Random.Range(0f, 3f);
        faceToTheRight = true;
    }
    void CalculateNewMovementVectorWhenFishHitTheRight()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * Random.Range(0f, 3f);
        faceToTheRight = false;
    }
    void RotateThis()
    {
        if (faceToTheRight)
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 1);
        else
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 1);
    }
}
