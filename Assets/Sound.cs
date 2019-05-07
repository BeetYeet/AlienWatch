using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
	public static Sound i
	{
		get; private set;
	}
	public AudioSource SfxSource, SFXsource2;
	public AudioSource DeathSource;
	public List<AudioClip> audios;
	// Start is called before the first frame update
	void Start()
	{
		i = this;

		PlayerBaseClass.current.playerMelee.SwingStart +=
		() =>
		{
			SfxSource.PlayOneShot(audios[0]);
		};
		

		GameController.curr.OnPlayerDeath += OnDeath;

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void DrinkSound()
	{
		SFXsource2.PlayOneShot(audios[2]);
	}
	void OnDeath()
	{
		DeathSource.PlayOneShot(audios[1]);
	}
}
