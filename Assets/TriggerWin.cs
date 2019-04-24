using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
	Animator Animator;
	private void Start()
	{
		Animator = GetComponent<Animator>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject == PlayerBaseClass.current.gameObject && BossDie.GreenBossDead && BossDie2.redBossDead && BossDie3.yellowBossDead)
		{
			Time.timeScale = 0f;
			TriggerWinScene();
		}
	}
	void TriggerWinScene()
	{
		Animator.SetBool("Win", true);
	}
	void Loadscene()
	{

	}
}
