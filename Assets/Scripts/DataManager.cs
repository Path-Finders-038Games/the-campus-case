using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Navigation
{
    public static class DataManager
    {
        private static MinigameName[] Minigames => Enum.GetValues(typeof(MinigameName)) as MinigameName[];
        private static string MinigameNameToString(MinigameName minigame) => $"MinigameStatus_{Enum.GetName(typeof(MinigameName), minigame)}";
        
        
        public static MinigameName SelectedMinigame;
        
        static DataManager()
        {
            MinigameStatus = new Dictionary<MinigameName, bool>();
            
            foreach (MinigameName minigame in Minigames)
            {
                MinigameStatus.Add(minigame, false);
            }
            
            SelectedMinigame = MinigameName.None;
        }
        
        public static Dictionary<MinigameName, bool> MinigameStatus
        {
            get
            {
                return Minigames.ToDictionary(minigame => minigame, minigame => PlayerPrefs.GetInt(MinigameNameToString(minigame)) == 1);
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