using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string _name;

    public AudioClip _audioClip;

    [Range(0f,1f)]
    public float _vuluom;

    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource _source;

    public bool _loop;
}
