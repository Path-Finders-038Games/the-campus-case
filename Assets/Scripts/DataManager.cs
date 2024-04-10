using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public static class DataManager
    {
        public static Dictionary<MinigameName, bool> MinigameStatus;
        public static MinigameName SelectedMinigame;
        
        static DataManager()
        {
            MinigameStatus = new Dictionary<MinigameName, bool>();
            foreach (MinigameName minigame in Enum.GetValues(typeof(MinigameName)))
            {
                MinigameStatus.Add(minigame, false);
            }
            
            SelectedMinigame = MinigameName.None;
        }
        
        public static void SetMinigameStatus(MinigameName minigame, bool status)
        {
            MinigameStatus[minigame] = status;
        }
        
        public static int CurrentStep
        {
            get => PlayerPrefs.GetInt("Currentstep");
            set => PlayerPrefs.SetInt("Currentstep", value);
        }

        public static string CurrentMap
        {
            get => PlayerPrefs.GetString("Currentmap");
            set => PlayerPrefs.SetString("Currentmap", value);
        }
        
        public static string Language
        {
            get => PlayerPrefs.GetString("Language");
            set => PlayerPrefs.SetString("Language", value);
        }
        
        public static string Buddy
{
            get => PlayerPrefs.GetString("Buddy");
            set => PlayerPrefs.SetString("Buddy", value);
        }
    }
}