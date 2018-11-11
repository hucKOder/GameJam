using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantSpawner : MonoBehaviour {

	public Transform destination; 
	public GameObject servantPrefab;
	public float radius = 2f;

	// Use this for initialization
	void Start () {
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
    {
		for (var i = 0; i < (int) (DataHandler.Followers / 10); i++) {
			var servant = Instantiate(servantPrefab, transform.position, transform.rotation);
			var mh = servant.GetComponent<MovementHandler>();
			var tempVect = new Vector2(destination.position.x, destination.position.y) + UnityEngine.Random.insideUnitCircle * radius;
			mh.destination = new Vector3(tempVect.x, tempVect.y, 0f);
			servant.GetComponent<ServantController>().destination = new Vector3(tempVect.x, tempVect.y, 0f);
        	yield return new WaitForSecondsRealtime(1);
		}
    }
	
	// Update is called once per frame
	void Update () {
	}
}
