using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
	public AudioSource SfxSource;
	public AudioSource DeathSource;
	public List<AudioClip> audios;
	// Start is called before the first frame update
	void Start()
	{
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

	void OnDeath()
	{
		DeathSource.PlayOneShot(audios[1]);
	}
}
