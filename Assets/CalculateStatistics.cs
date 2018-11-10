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
    }

    private void Affection()
    {
        
    }

    private void Followers()
    {
        
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
        DataHandler.Forecast = UnityEngine.Random.Range(0, 3);
    } 
}
