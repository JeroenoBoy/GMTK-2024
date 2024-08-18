using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;
using JUtils;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    public Sound[] _sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        foreach (var sound in _sounds)
        {
            sound._source = gameObject.AddComponent<AudioSource>();
            sound._source.clip = sound._audioClip;

            sound._source.volume = sound._vuluom;
            sound._source.pitch = sound.pitch;

            sound._source.loop = sound._loop;
        }
    }

    private void Start()
    {
        Play("Start");
    }

    public void Play(String name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);
        if (s == null)
            return;
        s._source.Play();
    }
}
