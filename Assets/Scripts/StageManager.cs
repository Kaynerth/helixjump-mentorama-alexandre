using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName = "New Stage")]
public class StageManager : ScriptableObject
{
    public Color stageBackgroundColor = Color.white;
    public Color stageFloorsColor = Color.white;
    public Color stageBallColor = Color.white;

    public List<Level> floors = new List<Level>();
}
