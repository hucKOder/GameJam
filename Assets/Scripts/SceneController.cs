using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

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
