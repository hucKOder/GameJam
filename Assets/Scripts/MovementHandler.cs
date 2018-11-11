using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{

    public Vector3 destination;
    public float speed = 5.0f;

    // Use this for initialization
    void Start()
    {
        //destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        GetComponent<VisualHandler>().SetDireciton(destination);

        if (Mathf.Abs(transform.position.x - destination.x) < 0.1f)
        {
            GetComponent<Animator>().SetInteger("State", 0);

        }
        else
        {
            GetComponent<Animator>().SetInteger("State", 1);

        }
    }
}
