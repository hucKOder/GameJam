using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour {

    public Animator animator;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
        {
            FadeToLevel(2);
        }
	}

    public void FadeToLevel(int setLevel)
    {
        animator.SetTrigger("FadeIn");
    }
}
