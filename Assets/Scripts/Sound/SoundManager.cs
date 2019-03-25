﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{

    static Dictionary<string, float> soundTimerDictionary;
    public static void Initalize()
    {
        soundTimerDictionary = new Dictionary<string, float>();
        soundTimerDictionary["footstep"] = 0;
    }
    public static void PlaySound(string soundName)
    {
        if (CanPlaySound(soundName))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(soundName));
        }
    }

    static bool CanPlaySound(string sound)
    {
        GameAssets.ClipType clipType;
        clipType = GameAssets.curr.GetClip(sound);
        if (clipType == null)
            return false;
        switch (clipType.behavior)
        {
            default:
                return true;
            case GameAssets.ClipBehavior.Loop:
               if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playherMoveMax = .05f;
                    if(lastTimePlayed + playherMoveMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }                
        }
    }

    static AudioClip GetAudioClip(string sound)
    {
        GameAssets.ClipType clip = GameAssets.curr.GetClip(sound);
        if(clip != null)
        {
            return clip.clips[Random.Range(0, clip.clips.Count-1)];
        }
        Debug.LogWarning("Sound " + sound + "not Found!");
        return null;
    }
}