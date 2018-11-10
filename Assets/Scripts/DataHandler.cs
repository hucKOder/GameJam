using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataHandler
{

    private static List<Dialog> dialogJSONS = new List<Dialog>();
    private static int followers = 5;
    private static float godsAffection = 1;

    private static bool wasInMinigame;
    private static int numberOfDemons;
    private static int killedDemons;
    private static int day;
    private static int weather;
    private static int forecast;

    public static List<Dialog> DialogJSONS
    {
        get
        {
            return dialogJSONS;
        }
    }

    public static int Followers
    {
        get
        {
            return followers;
        }
        set
        {
            followers = value;
        }
    }

    public static float GodsAffection
    {
        get
        {
            return godsAffection;
        }
        set
        {
            godsAffection = value;
        }
    }

    public static bool WasInMinigame
    {
        get
        {
            return wasInMinigame;
        }
        set
        {
            wasInMinigame = value;
        }
    }

    public static int NumberOfDemons
    {
        get
        {
            return numberOfDemons;
        }
        set
        {
            numberOfDemons = value;
        }
    }

    public static int KilledDemons
    {
        get
        {
            return killedDemons;
        }
        set
        {
            killedDemons = value;
        }
    }

    public static int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }

    public static int Weather
    {
        get
        {
            return weather;
        }
        set
        {
            weather = value;
        }
    }

    public static int Forecast
    {
        get
        {
            return forecast;
        }
        set
        {
            forecast = value;
        }
    }

    public static void Init()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = di.GetFiles("*.json");

        foreach (var path in files)
        {
            string filePath = path.FullName;
            if (File.Exists(filePath))
            {
                // Read the json from the file into a string
                using (StreamReader stream = new StreamReader(filePath))
                {
                    string json = stream.ReadToEnd();
                    // Pass the json to JsonUtility, and tell it to create a GameData object from it
                    dialogJSONS.Add(JsonUtility.FromJson<Dialog>(json));
                }
            }
        }
    }

    public static string getWeatherString(int number)
    {
        switch (number)
        {
            case 0:
                return "Rainy";
            case 1:
                return "Windy";
            case 2:
                return "Sunny";
            case 3:
                return "Freezing";
        }
        return "Unknown weather";
    }
}
