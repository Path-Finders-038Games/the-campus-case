using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationFile 
{
    public string Name;
    public string Description;
    public List<string> Facts = new();
    public string HintNextLocation;
    public bool IsCompleted;
    public LocationFile(string name, string description,List<string> fatcs, string hintNextLocation, bool isCompleted)
    {
        Name = name;
        Description = description;
        Facts = fatcs;
        HintNextLocation = hintNextLocation;
        IsCompleted = isCompleted;
    }
}
