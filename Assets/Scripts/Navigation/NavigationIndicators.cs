using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Navigation
{
    /// <summary>
    /// Logic for the navigation indicators, including numbers for uncompleted minigames in the area.
    /// </summary>
    public class NavigationIndicators : MonoBehaviour
    {
        public MinigameName[] MinigameDependencies = Array.Empty<MinigameName>();
        public TMP_Text Indicator;

        private int _uncompletedMinigames;

        public NavigationIndicators()
        {
            // Remove duplicates from the array of minigame dependencies.
            MinigameDependencies = MinigameDependencies.Distinct().ToArray();
        }

        private void Start()
        {
            // If no indicator is set, throw an error.
            if (Indicator == null)
            {
                Debug.LogError("Indicator is not set.");
                throw new NullReferenceException("Indicator is not set.");
            }

            // If there are no uncompleted minigames, hide the indicator.
            if (_uncompletedMinigames == 0)
            {
                Indicator.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (MinigameDependencies.Length == 0) return;
            
            // If the number of uncompleted minigames has not changed, return.
            // This way we prevent unnecessary UI updates.
            int uncompletedMinigames = GetUncompletedMinigames;
            if (_uncompletedMinigames == uncompletedMinigames) return;
            _uncompletedMinigames = uncompletedMinigames;

            // If there are no uncompleted minigames, hide the indicator.
            Indicator.gameObject.SetActive(_uncompletedMinigames > 0);
            
            // Update the number of uncompleted minigames in the area.
            Indicator.text = uncompletedMinigames.ToString();
        }

        /// <summary>
        /// Returns the number of uncompleted minigames in the area.
        /// </summary>
        private int GetUncompletedMinigames =>
            MinigameDependencies.Count(minigame => !DataManager.MinigameStatus[minigame]);
    }
}