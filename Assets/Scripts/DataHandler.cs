using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataHandler
{

    private static List<Dialog> dialogJSONS = new List<Dialog>();
    private static int followers = 20;
    private static float godsAffection = 1;

    private static bool wasInMinigame = false;
    private static int numberOfDemons;
    private static int killedDemons;
    private static int day = 1;
    private static int weather = 0;
    private static int forecast = 0;
    private static int[] reward = new int[4] { 0, 0, 0, 0 };
    private static int[] penalty = new int[4] { 0, 0, 0, 0 };
    private static int weatherChoice = 0;
    private static int enemiesKilledAllTime = 0;

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

    public static int WeatherChoice
    {
        get
        {
            return weatherChoice;
        }

        set
        {
            weatherChoice = value;
        }
    }

    public static int[] Penalty
    {
        get
        {
            return penalty;
        }

        set
        {
            penalty = value;
        }
    }

    public static int[] Reward
    {
        get
        {
            return reward;
        }

        set
        {
            reward = value;
        }
    }

    public static int EnemiesKilledAllTime
    {
        get
        {
            return enemiesKilledAllTime;
        }
        set
        {
            enemiesKilledAllTime = value;
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
