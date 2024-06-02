using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;

    public static int song = 0;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }

        if (song == 0)
        {
            if (!songIsPlaying("MainThemeMenu"))
            {
                stopSound("MainThemeGame");
                playSound("MainThemeMenu");
            }
        }
        else
        {
            stopSound("MainThemeMenu");
            playSound("MainThemeGame");
        }
    }

    public bool songIsPlaying(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
               return sound.source.isPlaying;
            }
        }
        return false;
    }

    public void playSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Play();
            }
        }
    }

    public void stopSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Stop();
            }
        }
    }
}
