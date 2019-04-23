using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().startDialogue(dialogue);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == PlayerBaseClass.current.gameObject)
			TriggerDialogue();
	}
}
