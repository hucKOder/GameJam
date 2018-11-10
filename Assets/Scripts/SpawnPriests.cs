using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPriests : MonoBehaviour {

    public CameraHandler cameraHandler;

    public List<GameObject> peasants;

    public GameObject peasantPrefab;
    public int numberOfPeasants = 3;
    public Transform destination;
    public Transform spawnPoint;

    public float positionOffset = 0.3f;
    public float randomOffsetX = 0.1f;
    public float randomOffsetY = 0.05f;

    public float spawnOffset = 2f;
    public float minSpeed = 0.01f;
    public float maxSpeed = 0.02f;

    int currentPeasant = 0;

    DialogTrigger dialogTrigger;
    

    private bool peasantsSpawned = false;

    // Update is called once per frame
    void Update () {
        if (!peasantsSpawned && cameraHandler.gameIsReady) {
            SpawnPeasants();
            peasantsSpawned = true;
        }

        if (Vector3.Distance(peasants[0].transform.position, destination.position) < 0.001)
        {
            StartDialog(peasants[0]);
        }
    }

    void SpawnPeasants()
    {
        peasants = new List<GameObject>();

        for (var i = 0; i < numberOfPeasants; i++)
        {
            GameObject peasant = Instantiate(peasantPrefab, spawnPoint.position + new Vector3(i * spawnOffset, 0, 0), Quaternion.LookRotation(-Vector3.forward, Vector3.up));
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
        dialogTrigger.TriggerDialogue();

        currentPeasant++;
    }
}
