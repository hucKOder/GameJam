using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManger : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialog dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");    
    }
}
