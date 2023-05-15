using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This scripts will contain many function that will be use all over the game for object that will swimming around
public class FishDumbMoveAround : MonoBehaviour
{
    [SerializeField] private FishDriversManager fishDriversManager;
    
    private float latestDirectionChangeTime;
    private float directionChangeTime; // the amount of time the fist stay in one direction
   
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    
    [SerializeField] private float fishDumbVelocity = 2f;
    [SerializeField] private float maxTime; //the max amount of time the fish can use to spent in one direction
    [SerializeField] private float minTime; //the minimum amount of time the fish can use to spent in one direction
    [SerializeField] private List<string> tagsToAvoid = new List<string>();


    //need 4 gameobjects in the hierarchy
    private GameObject top;
    private GameObject bottom;
    private GameObject right;
    private GameObject left;

    //[SerializeField] private FishFollowFood fishFollowFood;
    
    void Start(){
        //get all the position of top, bottom, left and right side of the tank by find the object i put at the ech of the player screen 
        top = GameObject.Find("Top"); //NAME
        bottom = GameObject.Find("Bottom");
        right = GameObject.Find("Right");
        left = GameObject.Find("Left");
        
        latestDirectionChangeTime = 0f;
        //make the fish change direction after a random amount of time 
        directionChangeTime = Random.Range(1.5f, 5.5f);
        CalculateNewMovementVector(out movementDirection, out movementPerSecond, out fishDriversManager.faceToTheRight);
    }
 
    public void MoveNormalFunction()
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
            CalculateNewMovementVector(out movementDirection, out movementPerSecond, out fishDriversManager.faceToTheRight);
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
    //make the fish change direction if it touch other fish
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagsToAvoid.Count; i++)
        {
            if (other.gameObject.CompareTag(tagsToAvoid[i])) 
            {
                // do things ;)
                CalculateNewMovementVector(out movementDirection, out movementPerSecond, out fishDriversManager.faceToTheRight);
            }
        }
    }

    public void CalculateNewMovementVector(out Vector2 moveDirec, out Vector2 moveSpeed, out bool faceRight)
    {
        bool check;
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the fish
        moveDirec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        if (moveDirec.x < 0) //the fish is move to the left
        {
            //fishFollowFood.MakeFishRotateToTheLeft();
            check = false;
        }
        else if (moveDirec.x > 0) //the fish is move to the right
        {
            //fishFollowFood.MakeFishRotateToTheRight();
            check = true;
        }
        else
        {
            check = true;
        }
        

        moveSpeed = moveDirec * fishDumbVelocity * Random.Range(0f, 1f);
        faceRight = check;
    }


    void CalculateNewMovementVectorWhenFishHitTheLeft()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        fishDriversManager.faceToTheRight = true;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        
    }
    void CalculateNewMovementVectorWhenFishHitTheRight()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f)).normalized;
        fishDriversManager.faceToTheRight = false;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        
    }
    void CalculateNewMovementVectorWhenFishHitTheTop()
    {
        //create a random direction but not  to the top vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f)).normalized;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        if (movementDirection.x < 0) //the fish is move to the left
        {
            fishDriversManager.faceToTheRight = false;
        }

        if (movementDirection.x > 0)//the fish is move to the right
        {
            fishDriversManager.faceToTheRight = true;
        }
    }
    void CalculateNewMovementVectorWhenFishHitTheBottom()
    {
        //create a random direction but not  to the left vector with the magnitude of 1, later multiply it with the velocity of the fish
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * fishDumbVelocity * Random.Range(0f, 1f);
        if (movementDirection.x < 0) //the fish is move to the left
        {
            fishDriversManager.faceToTheRight = false;
        }

        if (movementDirection.x > 0)//the fish is move to the right
        {
            fishDriversManager.faceToTheRight = true;
        }
    }
}
