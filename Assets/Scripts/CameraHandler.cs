﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

    [HideInInspector]
    public bool gameIsReady = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameIsReady)
        {
            Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 1);

            if (Quaternion.Equals(transform.rotation, finalRotation))
            {
                gameIsReady = true;
            }
        }
    }
}