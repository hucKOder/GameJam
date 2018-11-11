using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour {

    public Image prayerBox;
    public Text followersText;
    public Text affectionText;
    public Sprite[] actualWeather;
    public Image weatherSprite;

    private bool transitionReady = false;
    [HideInInspector]
    public bool gameIsReady = false;
    [HideInInspector]
    public bool selectChoice = false;
    // Use this for initialization
    void Start () {
        affectionText.text = (DataHandler.GodsAffection * 100).ToString() + "%";
        followersText.text = DataHandler.Followers.ToString() + " followers";

        ShowForecastWeather();
    }

    private void ShowForecastWeather()
    {
        DataHandler.Forecast = UnityEngine.Random.Range(0, 4);
        weatherSprite.GetComponent<Image>().sprite = actualWeather[DataHandler.Forecast];
    }

    // Update is called once per frame
    void Update () {
        if (!gameIsReady)
        {
            Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 1);

            if (Quaternion.Equals(transform.rotation, finalRotation))
            {
                gameIsReady = true;
            }
        }
        if (selectChoice && !transitionReady)
        {
            Quaternion finalRotation = Quaternion.LookRotation(Vector3.up, -Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 1);
            if (Quaternion.Equals(transform.rotation, finalRotation))
            {
                transitionReady = true;
                prayerBox.gameObject.SetActive(true);
            }
        }
    }
}
