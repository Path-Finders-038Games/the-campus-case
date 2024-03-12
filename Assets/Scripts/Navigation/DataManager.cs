using UnityEngine;

namespace Navigation
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        public int CurrentStep;
        public string CurrentMap;

        /// <summary>
        /// Initializes the instance and sets the current map and step to the saved values.
        /// </summary>
        private void Awake()
        {
            // If the instance is null, set it to this object and don't destroy it on load, otherwise destroy the object.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Set the current map and step to the saved values.
            CurrentMap = PlayerPrefs.GetString("Currentmap");
            CurrentStep = PlayerPrefs.GetInt("Currentstep");
        }
    }
}