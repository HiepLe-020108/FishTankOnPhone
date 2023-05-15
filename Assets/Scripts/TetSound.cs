using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zindeaxx.SoundSystem;
//remember this so you can use this in the future for any sound effect
public class TetSound : MonoBehaviour
{
    [SerializeField] private SoundOnEnable soundOnEnable;
    public bool testOn;
    private void Update()
    {
        if (testOn)
        {
            soundOnEnable.OnEnable();
        }
        
    }
}
