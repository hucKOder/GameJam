using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataHandler
{

    private static List<Dialog> dialogJSONS = new List<Dialog>();
    //private static int deaths, assists, points;

    public static List<Dialog> DialogJSONS
    {
        get
        {
            return dialogJSONS;
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
}
