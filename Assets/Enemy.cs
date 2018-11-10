using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float movementSpeed = 0.01f;

    private GameObject fighter;

	// Use this for initialization
	void Start () {
        fighter = GameObject.FindGameObjectWithTag("Fighter");
    }
	
	// Update is called once per frame
	void Update () {
        if (fighter != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, fighter.transform.position, movementSpeed);
        }
	}
}
