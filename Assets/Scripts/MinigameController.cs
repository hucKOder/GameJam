using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour {

    public int minEnemies = 2;
    public int enemiesCoeff = 4;
    public float spawnRadius = 2.5f;
    public int spawnAtBeginning = 3;

    private int spawnedDemons = 0;
    private int numberOfEnemies;

	// Use this for initialization
	void Start () {
        DataHandler.WasInMinigame = true;
        var affection = DataHandler.GodsAffection;
        numberOfEnemies =  minEnemies + (int) (Mathf.Round(enemiesCoeff / affection)) - enemiesCoeff;

        DataHandler.NumberOfDemons = numberOfEnemies;

        for (var i = 0; i < Mathf.Min(spawnAtBeginning, numberOfEnemies); i++)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
