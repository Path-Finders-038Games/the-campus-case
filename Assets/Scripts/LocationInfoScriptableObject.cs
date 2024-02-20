using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationData", menuName = "ScriptableObjects/LocationInfoScriptableObject", order = 1)]
public class LocationInfoScriptableObject : ScriptableObject
{

    public LocationData Data_NL;

    public LocationData Data_EN;

    public string Name;
    public string Description;
    public List<string> Facts = new();
    public string HintNextLocation;
}

[Serializable]
public class LocationData
{
    public string Name;
    [TextArea(0,5)]
    public string Description;
    [TextArea(0, 5)]
    public List<string> Facts = new();
    [TextArea(0, 5)]
    public string HintNextLocation;
}

