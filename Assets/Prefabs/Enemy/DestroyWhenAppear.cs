using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//object for enemy to spawn, then destroy it self
public class DestroyWhenAppear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this);
    }

}