using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable] 
public class AudioFile
{
    public string audioName;
    
    [BoxGroup("Settings"), Range(0f, 1f)] public float volume = 1f;
    [BoxGroup("Settings"), Range(-3f, 3f)] public float pitch = 1f;
    [BoxGroup("Settings")] public bool looping;
    
    [SerializeField, HideLabel, BoxGroup("Audio")] 
    private WeightedRandomList<AudioClip> clips;
    
    public AudioClip GetAudioClip() => clips.GetRandom();
}

[CreateAssetMenu(menuName = "System/Audio Scriptable object", fileName = "New Audio scriptable object")]
public class SO_Audio : ScriptableObject
{
    public AudioMixerGroup mixerGroup;
    
    [SerializeField, Searchable] private List<AudioFile> _files;
    public AudioFile FindAudioByName(string audioName) => _files.SingleOrDefault(file => file.audioName == audioName);
}