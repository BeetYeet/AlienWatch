using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandlerMenu : MonoBehaviour
{
	public AudioSource AudioSource;
	public AudioClip AudioClip;
	public void HoverSound()
	{
		AudioSource.PlayOneShot(AudioClip);
	}
	
}
