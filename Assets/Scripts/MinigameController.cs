using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour {

    public int minEnemies = 2;
    public int enemiesCoeff = 4;
    public float spawnRadius = 2.5f;
    public int spawnAtBeginning = 3;
    public float spawnCoolDown = 1.5f;
    public GameObject enemyPrefab;

    private int spawnedDemons = 0;
    private int numberOfEnemies;
    private float spawnTimer;


	// Use this for initialization
	void Start () {
        DataHandler.WasInMinigame = true;
        var affection = DataHandler.GodsAffection;
        numberOfEnemies =  minEnemies + (int) (Mathf.Round(enemiesCoeff / affection)) - enemiesCoeff;

        DataHandler.NumberOfDemons = numberOfEnemies;

        for (var i = 0; i < Mathf.Min(spawnAtBeginning, numberOfEnemies); i++)
        {
            Vector2 circle = Random.insideUnitCircle;
            circle.Normalize();
            Instantiate(enemyPrefab, circle, Quaternion.identity);
            spawnedDemons++;
        }
        spawnTimer = Time.time + spawnCoolDown;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnedDemons < numberOfEnemies && spawnTimer < Time.time)
        {
            Vector2 circle = Random.insideUnitCircle;
            circle.Normalize();
            Instantiate(enemyPrefab, circle, Quaternion.identity);
            spawnedDemons++;
        }
	}
}
