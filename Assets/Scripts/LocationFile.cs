using System.Collections.Generic;

public class LocationFile
{
    public string Name;
    public string Description;
    public List<string> Facts;
    public string HintNextLocation;
    public bool IsCompleted;

    public LocationFile(string name, string description, List<string> facts, string hintNextLocation, bool isCompleted)
    {
        Name = name;
        Description = description;
        Facts = facts;
        HintNextLocation = hintNextLocation;
        IsCompleted = isCompleted;
    }
}