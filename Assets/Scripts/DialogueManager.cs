using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Image dialogBox;
    Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
    }

    public void StartDialogue(Sentence sentence, string name)
    {
        dialogBox.enabled = true;
        nameText.enabled = true;
        dialogueText.enabled = true;
        nameText.text = name;
        dialogueText.text = sentence.text;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        var sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
