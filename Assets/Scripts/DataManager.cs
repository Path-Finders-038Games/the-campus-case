using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataManager
{
    private static MinigameName[] Minigames =>
        (Enum.GetValues(typeof(MinigameName)) as MinigameName[] ?? Array.Empty<MinigameName>())
        .Where(m => m != MinigameName.None).ToArray();

    private static string MinigameNameToString(MinigameName minigame) =>
        $"MinigameStatus_{Enum.GetName(typeof(MinigameName), minigame)}";


    public static MinigameName SelectedMinigame;

    static DataManager()
    {
        SelectedMinigame = MinigameName.None;
        CameraPosition = new Vector3(0, 2.689579f, 0);
    }

    public static float CameraOrthographicSize
    {
        get => PlayerPrefs.GetFloat("CameraOrthographicSize");
        set => PlayerPrefs.SetFloat("CameraOrthographicSize", value);
    }

    public static Vector3 CameraPosition
    {
        get => new(PlayerPrefs.GetFloat("CameraPositionX"), PlayerPrefs.GetFloat("CameraPositionY"),
            PlayerPrefs.GetFloat("CameraPositionZ"));
        set
        {
            PlayerPrefs.SetFloat("CameraPositionX", value.x);
            PlayerPrefs.SetFloat("CameraPositionY", value.y);
            PlayerPrefs.SetFloat("CameraPositionZ", value.z);
        }
    }

    public static Dictionary<MinigameName, bool> MinigameStatus
    {
        get
        {
            return Minigames.ToDictionary(minigame => minigame,
                minigame => PlayerPrefs.GetInt(MinigameNameToString(minigame)) == 1);
        }
        private set
        {
            foreach (MinigameName minigame in Minigames)
            {
                PlayerPrefs.SetInt(MinigameNameToString(minigame), value[minigame] ? 1 : 0);
            }
        }
    }

    public static void SetMinigameStatus(MinigameName minigame, bool status)
    {
        Dictionary<MinigameName, bool> minigameStatus = MinigameStatus;
        minigameStatus[minigame] = status;
        MinigameStatus = minigameStatus;
    }
    
    public static void ResetAllMinigameStatus()
    {
        MinigameStatus = Minigames.ToDictionary(minigame => minigame, _ => false);
    }

    public static int CurrentStep
    {
        get => PlayerPrefs.GetInt("Currentstep") | 0;
        set => PlayerPrefs.SetInt("Currentstep", value);
    }

    public static string CurrentMap
    {
        get => PlayerPrefs.GetString("Currentmap");
        set => PlayerPrefs.SetString("Currentmap", value);
    }

    public static LanguageManager.Language Language
    {
        get => PlayerPrefs.GetString("Language") switch
        {
            "NL" => LanguageManager.Language.Dutch,
            "EN" => LanguageManager.Language.English,
            _ => LanguageManager.Language.None,
        };
        set => PlayerPrefs.SetString("Language", value switch
        {
            LanguageManager.Language.Dutch => "NL",
            LanguageManager.Language.English => "EN",
            _ => "",
        });
    }

    public static string Buddy
    {
        get => PlayerPrefs.GetString("Buddy");
        set => PlayerPrefs.SetString("Buddy", value);
    }

    public static bool DemoMode
    {
        get => PlayerPrefs.GetInt("DemoMode") == 1;
        set => PlayerPrefs.SetInt("DemoMode", value ? 1 : 0);
    }
}