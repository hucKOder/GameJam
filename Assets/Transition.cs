using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public float transitionTime = 5f;
    public SceneController sceneController;

    private float timer;

    private void Start()
    {
        timer = Time.time + transitionTime;
    }

    // Update is called once per frame
    void Update () {
		if (timer < Time.time)
        {
            sceneController.LoadNextScene();
        }
	}
}
