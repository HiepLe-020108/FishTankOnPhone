using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayBackGroundMusic : MonoBehaviour
{
    public AudioSetting audioSetting;
    // Start is called before the first frame update
    void Start()
    {
        audioSetting.Play("BackGroundMusic", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
