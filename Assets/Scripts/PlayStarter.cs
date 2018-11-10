using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayStarter : MonoBehaviour {

    AudioSource audioSource;
    bool startGame = false;

    private void Start()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();    
    }

    public void StartGame()
    {
        startGame = true;
        //DataHandler.Init();
        //SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (startGame)
        {
            audioSource.volume -= 1f * Time.deltaTime;
        }
        if (audioSource.volume == 0f)
        {
            DataHandler.Init();
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator FadeOutMusic()
    {
        while (audioSource.volume >= 0)
        {
            audioSource.volume -= 1f * Time.deltaTime; 
        }
        yield return new WaitForSeconds(1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
