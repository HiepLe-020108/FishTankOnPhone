using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveChildComponent : MonoBehaviour
{
    public ClickToMove parentScript;

    //call this function when the gameobejct get click
    //this will call the logic of Parent of the game object that this script is attach to
    void OnMouseDown()
    {
        parentScript.OnChildClicked(gameObject);
    }
}
