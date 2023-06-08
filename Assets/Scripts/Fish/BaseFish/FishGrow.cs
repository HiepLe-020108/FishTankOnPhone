using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will change size of fish and also change it tag, from smallFish to bigFish
public class FishGrow : MonoBehaviour
{
    [SerializeField] private bool canChangeSize;

    [SerializeField] private float scaleChangeSpeed = 10f;
    private Vector3 scaleChange, scaleChangeBig, scaleChangeAmount;

    [SerializeField] private FishDriversManager fishDriversManager;

    private float startTime; //to check the amount of time the fish have been existed
    [SerializeField] private string tagChangeTo;
    private bool fishHasGrown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; //set the thing to the first frame of the object been create
        
        scaleChange = new Vector3(0.1f, .1f, .1f);
        scaleChangeBig = new Vector3(0.2f, 0.2f, 0.2f);
        scaleChangeAmount = new Vector3(0.001f, 0.001f, 0.001f);
    }


    private void Update()
    {
        if (Time.time - startTime >= fishDriversManager.fishTypeSO.timeNeedToGrow && fishHasGrown == false)
        {
            //grow fish
            fishHasGrown = true;
        }

        if (transform.localScale.x > scaleChangeBig.x)
        {
            //if the fish big enough the stop growing
            fishHasGrown = false;
        }
        if (fishHasGrown)
        {
            GrowFish();
        }
    }



    void GrowFish()
    {
        transform.localScale += scaleChangeAmount * Time.deltaTime * scaleChangeSpeed;
                gameObject.tag = tagChangeTo;
    }
}
