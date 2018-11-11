using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodWill : MonoBehaviour
{

    public GameObject priest;
    public GameObject gore;

    private MovementHandler mh;

    // Use this for initialization
    void Start()
    {
        mh = GetComponent<MovementHandler>();
        mh.destination = new Vector3(0.979f, 0.692f, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishHim()
    {
        mh.destination = new Vector3(0.979f, 0.692f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("som tu");
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Fighter")
        {
            Instantiate(gore, other.transform.position, other.transform.rotation);

            other.gameObject.SetActive(false);
        }
    }
}
