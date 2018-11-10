using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateStatistics : MonoBehaviour {

    public Text weatherText;
    public Text followersText;
    public Text affectionText;

    private void Start()
    {
        Weather();
        Followers();
        Affection();

        if (DataHandler.WasInMinigame)
        {
            DataHandler.EnemiesKilledAllTime += DataHandler.KilledDemons;
        }

        DataHandler.Reward = new int[4] { 0, 0, 0, 0 };
        DataHandler.Penalty = new int[4] { 0, 0, 0, 0 };
    }

    private void Affection()
    {
        if (DataHandler.WasInMinigame) {
            DataHandler.GodsAffection -= 0.07f;
            DataHandler.GodsAffection = Mathf.Clamp(DataHandler.GodsAffection, 0f, 1f);
            affectionText.text = DataHandler.GodsAffection.ToString() + " (-0.07)";
        }
        else
        {
            DataHandler.GodsAffection += 0.03f;
            affectionText.text = DataHandler.GodsAffection.ToString() + " (+0.03)";
        }
    }

    private void Followers()
    {
        // Weather is already calculated
        int raise = DataHandler.Reward[DataHandler.Weather] - DataHandler.Penalty[DataHandler.Weather];
        DataHandler.Followers += raise;

        if (raise >= 0)
        {
            followersText.text = DataHandler.Followers.ToString() + " (+" + raise.ToString() + ")";
        }
        else
        {
            followersText.text = DataHandler.Followers.ToString() + " (" + raise.ToString() + ")";
        }
    }

    public void Weather()
    {
        float rnd;

        if (DataHandler.WasInMinigame)
        {
             rnd = UnityEngine.Random.Range(0f,1f);

            if (rnd < DataHandler.KilledDemons/DataHandler.NumberOfDemons)
            {
                DataHandler.Weather = DataHandler.WeatherChoice;
                DisplayInfo(weatherText, DataHandler.Weather);
                return;
            }
         
        }
        rnd = UnityEngine.Random.Range(0f, 1f);

        if (rnd < 0.5f)
        {
            Debug.Log(rnd);
            DataHandler.Weather = DataHandler.Forecast;
        }
        else
        {
            DataHandler.Weather = UnityEngine.Random.Range(0, 4);
        }

        DisplayInfo(weatherText, DataHandler.Weather);
    }

    private void DisplayInfo(Text input, int value)
    {
        input.text = DataHandler.getWeatherString(value); 
    }

    public void GenerateNewWeather()
    {
        DataHandler.Forecast = UnityEngine.Random.Range(0, 4);
    } 
}
