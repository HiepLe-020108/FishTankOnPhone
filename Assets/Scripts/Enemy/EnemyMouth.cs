using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attach to an object on enemy that will touch target and destroy it after a delay
public class EnemyMouth : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag)) 
        {
            Destroy(other.gameObject);
        }
    }
}
