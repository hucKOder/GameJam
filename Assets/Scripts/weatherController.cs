using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherController : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(DataHandler.Weather);
		if (DataHandler.Weather == 0) {
			anim.Play("church_rain");
		}
		else {
			anim.Play("church_nothing");
		}
	}
}
