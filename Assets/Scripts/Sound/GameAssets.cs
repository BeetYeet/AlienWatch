using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	#region Static
	public static GameAssets curr;

    void Awake()
    {
		
        if (curr != null)
            throw new System.Exception("Too many instances of GameController, should only be one");
        curr = this;
    }
	#endregion

	#region Sound
	public List<ClipType> Clips;

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
	#endregion

	#region PopUpColor
	public Color hpDownColor;
	public Color hpUpColor;
	public Color ManaDownColor;
	public Color ManaUpColor;
	#endregion


	public Transform pfDamagePopup;
}