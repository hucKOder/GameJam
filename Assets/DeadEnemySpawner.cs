using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemySpawner : MonoBehaviour {

    public GameObject DeadEnemyPrefab;
    public float spawnCoolDown = 0.5f;

    private float spawnTimer = 0f;
    private int spawnedEnemies = 0;
    private int count;

	// Use this for initialization
	void Start () {
        count = DataHandler.EnemiesKilledAllTime;
	}

    private void Update()
    {
        if (spawnedEnemies < count && spawnTimer < Time.time)
        {
            Instantiate(DeadEnemyPrefab, transform.position, Random.rotation);
            spawnedEnemies++;
            spawnTimer = Time.time + spawnCoolDown;
        }
    }
}
