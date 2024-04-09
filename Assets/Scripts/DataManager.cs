using UnityEngine;

namespace Navigation
{
    public static class DataManager
    {
        private static int _currentStep = PlayerPrefs.GetInt("Currentstep");
        private static string _currentMap = PlayerPrefs.GetString("Currentmap");
        private static string _language = PlayerPrefs.GetString("Language");
        private static string _buddy = PlayerPrefs.GetString("Buddy");
        
        public static int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                PlayerPrefs.SetInt("Currentstep", value);
            }
        }

        public static string CurrentMap
        {
            get => _currentMap;
            set
            {
                _currentMap = value;
                PlayerPrefs.SetString("Currentmap", value);
            }
        }
    }
}