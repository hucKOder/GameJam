using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class VisualHandler : MonoBehaviour
{

    public GameObject VisualProvider;
    public SortingGroup sortingGroup;

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


    void Awake()
    {

        VisualProvider = GameObject.FindWithTag("BodyParts");
    }

    // Use this for initialization
    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
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

            LookRight();
            LookDown();

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
        Vector3 theScale = transform.localScale;
        if (theScale.x > 0)
            theScale.x *= -1;
        transform.localScale = theScale;
        //transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void LookRight()
    {
        Vector3 theScale = transform.localScale;
        if (theScale.x < 0)
            theScale.x *= -1;
        transform.localScale = theScale;
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Update()
    {
        sortingGroup.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    public void SetDireciton(Vector3 target)
    {

        var diff = target - transform.position;

        if (diff.x > 0)
        {
            LookRight();
        }
        else
        {
            LookLeft();
        }

        if (diff.y > 0)
        {
            LookUp();
        }
        else
        {
            LookDown();
        }

    }
}
