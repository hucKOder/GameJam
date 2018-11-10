using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public float transitionTime = 5f;
    public SceneController sceneController;
    AudioSource audioSource;

    private float timer;

    private void Start()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
        timer = Time.time + transitionTime;
    }

    // Update is called once per frame
    void Update () {
		if (timer < Time.time)
        {
            audioSource.volume -= 1f * Time.deltaTime;
        }
        if (audioSource.volume == 0f)
        {
            sceneController.LoadNextScene();
        }
    }
}
