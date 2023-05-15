using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShowItIsHungry : MonoBehaviour
{
    // private MeshRenderer cubeMeshRenderer;
    // [SerializeField] [Range(0f, 1f)] private float leftTime;
    // [SerializeField] private Color[] myColors;
    //
    // private int colorInt = 0;
    // private float t = 0f;
    // private int len;
    //
    // public bool forTest;

    [SerializeField] private GameObject bubbleThought;
    
    private void Start()
    {
        // cubeMeshRenderer = GetComponent<MeshRenderer>();
        // len = myColors.Length;
    }

    public void MakeBubbleThoughtAppear(bool isFishNormal, bool isFishHungry/*, bool isFishGoingToDie*/)
    {
        if (isFishNormal)
        {
            bubbleThought.SetActive(false);
            // cubeMeshRenderer.material.color =
            //     Color.Lerp(cubeMeshRenderer.material.color, myColors[0],
            //         leftTime * Time.deltaTime); //the fish will change color to the first color in the list(yellow)
        }
        if (isFishHungry)
        {
            bubbleThought.SetActive(true);
                // cubeMeshRenderer.material.color = Color.Lerp(cubeMeshRenderer.material.color, myColors[1],
                //     leftTime * Time.deltaTime); //the fish will change color to the second color in the list(yellow)
        }
        // if (isFishGoingToDie)
        // {
        //         cubeMeshRenderer.material.color = Color.Lerp(cubeMeshRenderer.material.color, myColors[2],
        //             leftTime * Time.deltaTime); //the fish will change color to the 3th color in the list(red)
        // }
        }
    }
//Code de tham khao, kha huu ich
// t = Mathf.Lerp(t, 1f, leftTime * Time.deltaTime);
// Debug.Log(t);
// if (t > .9f)
// {
//     t = 0f;
//     colorInt++;
//     colorInt = (colorInt >= len) ? 0 : colorInt;
// }