using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualHandler : MonoBehaviour
{
    public enum Direction
    {
        N = 0,
        W = 1,
        S = 2,
        E = 3,
    }

    public GameObject VisualProvider;

    private Direction _direction = Direction.E;
    public Direction direction
    {
        set
        {
            if (value == Direction.N && !lookingUp)
            {
                LookUp();
                lookingUp = true;
            }

            if (value == Direction.S && lookingUp)
            {
                LookDown();
                lookingUp = false;
            }

            if (value == Direction.W && !lookingLeft)
            {
                LookLeft();
                lookingLeft = true;
            }

            if (value == Direction.E && lookingLeft)
            {
                LookRight();
                lookingLeft = false;
            }
        }
        get
        {
            return _direction;
        }

    }

    private bool lookingUp = false;
    private bool lookingLeft = false;

    public int headID = 0;
    public int chestID = 0;
    public int legsID = 0;

    public GameObject HeadNode;
    public GameObject ChestNode;
    public GameObject UpperArmNodeR;
    public GameObject UpperArmNodeL;
    public GameObject ForearmNodeR;
    public GameObject ForearmNodeL;
    public GameObject PelvisNode;
    public GameObject ThighNodeR;
    public GameObject ThighNodeL;
    public GameObject CalfNodeR;
    public GameObject CalfNodeL;


    private Sprite head;
    private Sprite head_back;
    private Sprite chest;
    private Sprite chest_back;
    private Sprite pelvis;
    private Sprite pelvis_back;


    private bool init = false;

    // Use this for initialization
    void Start()
    {
        SetVisuals();
    }

    public void SetVisuals()
    {
        var manager = VisualProvider.GetComponent<BodyPartManager>();
        if (manager)
        {
            head = manager.heads[headID];
            head_back = manager.heads_back[headID];

            chest = manager.chests[chestID];
            chest_back = manager.chests[chestID];

            pelvis = manager.pelvises[legsID];
            pelvis_back = manager.pelvises_back[legsID];

            UpperArmNodeR.GetComponent<SpriteRenderer>().sprite = manager.upper_arms_r[chestID];
            UpperArmNodeL.GetComponent<SpriteRenderer>().sprite = manager.upper_arms_l[chestID];
            ForearmNodeR.GetComponent<SpriteRenderer>().sprite = manager.forearms_r[chestID];
            ForearmNodeL.GetComponent<SpriteRenderer>().sprite = manager.forearms_l[chestID];

            ThighNodeR.GetComponent<SpriteRenderer>().sprite = manager.thighs_r[legsID];
            ThighNodeL.GetComponent<SpriteRenderer>().sprite = manager.thighs_l[legsID];
            CalfNodeR.GetComponent<SpriteRenderer>().sprite = manager.calves_r[legsID];
            CalfNodeL.GetComponent<SpriteRenderer>().sprite = manager.calves_l[legsID];



        }
        else
        {
            Debug.LogError("No body manager.... sprites could not be loaded!");
        }
    }


    public void LookUp()
    {
        HeadNode.GetComponent<SpriteRenderer>().sprite = head_back;
        ChestNode.GetComponent<SpriteRenderer>().sprite = chest_back;
        PelvisNode.GetComponent<SpriteRenderer>().sprite = pelvis_back;
    }

    public void LookDown()
    {
        HeadNode.GetComponent<SpriteRenderer>().sprite = head;
        ChestNode.GetComponent<SpriteRenderer>().sprite = chest;
        PelvisNode.GetComponent<SpriteRenderer>().sprite = pelvis;
    }

    public void LookLeft()
    {
        transform.rotation = Quaternion.LookRotation(-Vector3.forward, Vector3.up);
    }

    public void LookRight()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

}
