using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    // Of NPC
    public int weather;
    public int reward;
    public int penalty;
    public int minimalDay;
    public string name;
    public Sentence[] sentences;
    public int[] pastWeather;
}

[System.Serializable]
public class Sentence
{
    public bool isPriest = false;
    public string text;
}
