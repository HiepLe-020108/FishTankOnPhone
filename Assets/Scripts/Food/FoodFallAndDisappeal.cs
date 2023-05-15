using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFallAndDisappeal : MonoBehaviour
{
    [SerializeField] private float fallingSpeed;

    
    //find the position of the bootom of the tank
    private GameObject bottom;
    // Start is called before the first frame update
    void Start()
    {
        bottom = GameObject.Find("Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > bottom.transform.position.y) //fall when the food is above the bottom of the tank
        {
            GetComponent<Transform>().position += new Vector3(0, -fallingSpeed * .01f, 0);
        }
        else
        {
            Destroy(gameObject, 5f);
        }
    }
    
}
