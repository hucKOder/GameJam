using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Triggers dialog we can create character and put this trigger on him

public class DialogTrigger : MonoBehaviour {

    public Dialog dialogue;

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogManger>().StartDialogue(dialogue);
    }
}
