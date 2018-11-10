using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


// Triggers dialog we can create character and put this trigger on him

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;
    public Transform meetingPoint;
    public float waitTime = 2.0f;
    DialogueManager priestDM;
    DialogueManager peopleDM;
    string gameDataFileName;
    int dialogueID;
    public int currentSentenceCounter = 0;

    bool isTalking = false;

    public void Spawn()
    {
        dialogue = new Dialog();

        priestDM = GameObject.Find("PriestDialogueManager").GetComponent<DialogueManager>();
        peopleDM = GameObject.Find("PeopleDialogueManager").GetComponent<DialogueManager>();

        LoadDialogData();
    }

    private void Update()
    {
        if (isTalking && Input.GetMouseButtonDown(0))
        {
            TriggerDialogue();
        }
    }

    public void EndDialog()
    {
        isTalking = false;
        priestDM.dialogBox.enabled = false;
        priestDM.nameText.enabled = false;
        priestDM.dialogueText.enabled = false;

        peopleDM.dialogBox.enabled = false;
        peopleDM.nameText.enabled = false;
        peopleDM.dialogueText.enabled = false;
    }

    public void TriggerDialogue()
    {
        isTalking = true;
        priestDM.dialogBox.enabled = false;
        priestDM.nameText.enabled = false;
        priestDM.dialogueText.enabled = false;

        peopleDM.dialogBox.enabled = false;
        peopleDM.nameText.enabled = false;
        peopleDM.dialogueText.enabled = false;

        Sentence sentence = new Sentence();
        if (currentSentenceCounter < dialogue.sentences.Length)
        {
            sentence = dialogue.sentences[currentSentenceCounter];
       
            if (sentence.isPriest)
            {
                priestDM.StartDialogue(sentence, "Priest");
            }
            else
            {
                peopleDM.StartDialogue(sentence, dialogue.name);
            }
        }
        currentSentenceCounter++;
    }

    private void LoadDialogData()
    {
        dialogueID = 1;
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build

        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "Dialog_" + dialogueID.ToString() + ".json");
        Debug.Log(filePath);
        
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            using (StreamReader stream = new StreamReader(filePath))
            {
                string json = stream.ReadToEnd();
                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                dialogue = JsonUtility.FromJson<Dialog>(json);
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }
}
