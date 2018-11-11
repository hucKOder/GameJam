using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour {

    public Vector3 destination;
    public float speed = 5.0f;

	// Use this for initialization
	void Start () {
        destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
        
        if (transform.position.x <= destination.x) {
        	GetComponent<Animator>().SetInteger("State", 0);
        }
	}
}
