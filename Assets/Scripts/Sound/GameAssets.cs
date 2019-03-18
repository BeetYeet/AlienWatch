using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    static GameAssets _Sound;

    public static GameAssets Sound
    {
        get
        {
            if (_Sound == null)
                _Sound = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _Sound;
        }
    }
    public List<ClipType> Clips;
    public static GameAssets curr;

    void Awake() // Awake() går innan Start()
    {
        if (curr != null)
            throw new System.Exception("Too many instances of GameController, should only be one");
        curr = this;
    }


    [System.Serializable]
    public class ClipType
    {
        public string denomenator;
        public ClipBehavior behavior;

        public List<AudioClip> clips;

    }
    public enum ClipBehavior
    {
        Loop,
        PlaySingle
    }

    public ClipType GetClip(string sound)
    {
        ClipType clip = null;
        foreach (ClipType c in Clips)
        {
            if (c.denomenator == sound)
            {
                clip = c;
            }
        }
        return clip;
    }
}