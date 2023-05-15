using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//this will handle movement and addFish function for this GameObject(FishMom)
public class GivesBirthBabyFish : MonoBehaviour
{
    [SerializeField] private GameObject fishGameObject;
    public bool ghuy;
    [SerializeField] private FishDumbMoveAround fishDumbMoveAround;
    public void Update()
    {
        fishDumbMoveAround.MoveNormalFunction();
        if (ghuy)
        {
            GameObject clone = Instantiate(fishGameObject, this.transform.position, Quaternion.identity);
        }
    }
}
