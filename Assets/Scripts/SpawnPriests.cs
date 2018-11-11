using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPriests : MonoBehaviour
{

    public CameraHandler cameraHandler;

    public List<GameObject> peasants;

    public GameObject peasantPrefab;
    public int numberOfPeasants = 3;
    public Transform destination;
    public Transform spawnPoint;
    public GameObject godWill;
    public SceneController sceneController;

    public float positionOffset = 0.3f;
    public float randomOffsetX = 0.1f;
    public float randomOffsetY = 0.05f;

    public float spawnOffset = 2f;
    public float minSpeed = 0.01f;
    public float maxSpeed = 0.02f;

    int currentPeasant = 0;

    DialogTrigger dialogTrigger;
    private bool inDialog = false;

    private bool peasantsSpawned = false;
    private bool isGameOver = false;

    void Start()
    {
        numberOfPeasants = Random.Range(1, 4);

        if (DataHandler.Followers <= 0 || DataHandler.Followers > 100 || DataHandler.GodsAffection <= 0)
        {
            isGameOver = true;
            godWill.SetActive(true);
            godWill.GetComponent<GodWill>().FinishHim();
            StartCoroutine(triggerGameOver());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {

        }
        else
        {
            if (!peasantsSpawned && cameraHandler.gameIsReady)
            {
                SpawnPeasants();
                peasantsSpawned = true;
            }
            if (currentPeasant != peasants.Count)
            {
                if (peasantsSpawned && !inDialog && Vector3.Distance(peasants[currentPeasant].transform.position, destination.position) <= 0.1)
                {
                    inDialog = true;
                    StartDialog(peasants[currentPeasant]);
                }
                else if (peasantsSpawned)
                {
                    peasants[currentPeasant].GetComponent<MovementHandler>().destination = destination.position;
                }
                if (peasantsSpawned && inDialog)
                {
                    if (dialogTrigger.dialogue.sentences.Length + 1 == dialogTrigger.currentSentenceCounter)
                    {
                        dialogTrigger.EndDialog();
                        inDialog = false;
                        peasants[currentPeasant].GetComponent<MovementHandler>().destination = spawnPoint.position;
                        for (var i = peasants.Count - 1; i > currentPeasant + 1; i--)
                        {
                            peasants[i].GetComponent<MovementHandler>().destination.x = peasants[i - 1].GetComponent<MovementHandler>().destination.x;
                        }
                        peasants[currentPeasant].transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                        currentPeasant++;
                    }
                }
            }
            if (peasantsSpawned && currentPeasant == peasants.Count)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    cameraHandler.selectChoice = true;
                }
            }
        }
    }

    void SpawnPeasants()
    {
        peasants = new List<GameObject>();

        for (var i = 0; i < numberOfPeasants; i++)
        {
            GameObject peasant = Instantiate(peasantPrefab, spawnPoint.position + new Vector3(i * spawnOffset, 0, 0), Quaternion.LookRotation(-Vector3.forward, Vector3.up));
            var visHandler = peasant.GetComponent<VisualHandler>();
            if (visHandler)
            {
                visHandler.chestID = Random.Range(2, 6);
                visHandler.headID = Random.Range(2, 6);
                visHandler.legsID = Random.Range(2, 6);
            }

            MovementHandler movementHandler = peasant.GetComponent<MovementHandler>();
            peasants.Add(peasant);

            if (movementHandler != null)
            {
                float speed = Random.Range(minSpeed, maxSpeed);
                movementHandler.speed = speed;
                movementHandler.destination = destination.position + new Vector3(i * positionOffset / numberOfPeasants + Random.Range(-randomOffsetX, randomOffsetX), Random.Range(-randomOffsetY, randomOffsetY) * numberOfPeasants, 0);
            }
        }
    }

    void StartDialog(GameObject peasant)
    {
        dialogTrigger = peasant.AddComponent<DialogTrigger>();
        dialogTrigger.Spawn();
        dialogTrigger.TriggerDialogue();
    }

    IEnumerator triggerGameOver()
    {
        yield return new WaitForSeconds(7f);
        sceneController.LoadSceneWithIndex(5);
    }
}
