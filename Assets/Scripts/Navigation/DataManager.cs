using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int CurrentStep;
    public string CurrentMap;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        CurrentMap = PlayerPrefs.GetString("Currentmap");
        CurrentStep = PlayerPrefs.GetInt("Currentstep");
    }
}


