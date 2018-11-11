using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantController : MonoBehaviour {

	public Vector3 destination;
	
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= destination.x && transform.position.y <= destination.y) { 
			animator.Play("ser_idle");
		}
	}
}
