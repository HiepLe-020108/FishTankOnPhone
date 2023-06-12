using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackGroundMusic : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.Play("BackGroundMusic", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
