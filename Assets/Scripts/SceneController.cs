using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public Animator animator;
    bool nextScene = false;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();    
    }

    private void Update()
    {
        if (nextScene)
        {
            audioSource.volume -= 1f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartFadeIn()
    {
        nextScene = true;
        animator.SetTrigger("FadeIn");
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneWithName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
