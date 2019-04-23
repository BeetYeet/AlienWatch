using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsManager : MonoBehaviour
{
	public UnityEngine.Audio.AudioMixer mix;
	public UnityEngine.UI.Slider[] Slider;
	private void Start()
	{
		mix.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
		Slider[0].value = PlayerPrefs.GetFloat("MasterVol");

		mix.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
		Slider[1].value = PlayerPrefs.GetFloat("MusicVol");

		mix.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
		Slider[2].value = PlayerPrefs.GetFloat("SFXVol");

		mix.SetFloat("AmbienceVol", PlayerPrefs.GetFloat("AmbienceVol"));
		Slider[3].value = PlayerPrefs.GetFloat("AmbienceVol");

	}

}
