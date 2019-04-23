using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	
	public TextMeshPro DialogueText;

	Queue<string> sentences;

    void Start()
    {
		sentences = new Queue<string>();
    }

	public void startDialogue (Dialogue dialogue)
	{
		

		sentences.Clear();

		foreach ( string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			endDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		DialogueText.text = sentence;
		StopAllCoroutines();
		StartCoroutine(typeSentence(sentence));
	}

	IEnumerator typeSentence(string sentence)
	{
		DialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			DialogueText.text += letter;
			yield return 0.1f;
		}
	}

	public void endDialogue()
	{
		Debug.Log("End of conversation");
	}
}

[System.Serializable]
public class Dialogue
{
	public string name;

	[TextArea(3, 10)]
	public string[] sentences;
}

