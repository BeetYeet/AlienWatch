using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetter : MonoBehaviour
{
	public AudioMixer MasterMixer;

	public void SetMasterVol(float MasterVol)
	{
		MasterMixer.SetFloat("MasterVol", MasterVol);
		PlayerPrefs.SetFloat("MasterVol", MasterVol);
	}
	public void SetMusicVol(float MusicVol)
	{
		MasterMixer.SetFloat("Musicvol", MusicVol);
		PlayerPrefs.SetFloat("Musicvol", MusicVol);
	}
	public void SetSFXVol(float SFXVol)
	{
		MasterMixer.SetFloat("SFXVol", SFXVol);
		PlayerPrefs.SetFloat("SFXVol", SFXVol);
	}
	public void SetAmbinetVol(float AmbinetVol)
	{
		MasterMixer.SetFloat("AmbienceVol", AmbinetVol);
		PlayerPrefs.SetFloat("AmbienceVol", AmbinetVol);
	}
	public void SetDialogueVol(float DialogueVol)
	{
		MasterMixer.SetFloat("DialogueVol", DialogueVol);
	}
}
