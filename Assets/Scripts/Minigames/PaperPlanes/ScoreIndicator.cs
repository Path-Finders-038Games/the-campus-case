using System;
using TMPro;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class ScoreIndicator : MonoBehaviour
    {
        public TMP_Text ScoreText;
        public GameObject ScoreContainer;

        /// <summary>
        /// Gets formatted score text.
        /// This contains the win and lose score.
        /// </summary>
        /// <returns></returns>
        private static string GetScoreText()
        {
            return $"Planes Hit: {PaperPlanesData.PlanesHit}/{PaperPlanesData.WinScore}\n" +
                   $"Planes Missed: {PaperPlanesData.PlanesMissed}/{PaperPlanesData.LoseScore}";
        }

        /// <summary>
        /// Starts the minigame. Runs before the first frame update.
        /// </summary>
        /// <exception cref="ArgumentNullException">ScoreText is not set in the Unity Editor.</exception>
        private void Start()
        {
            if (ScoreText == null)
            {
                throw new ArgumentNullException(nameof(ScoreText), "ScoreText is not set in the Unity Editor.");
            }
        }

        /// <summary>
        /// Displays the score container and updates the score text every frame.
        /// </summary>
        private void Update()
        {
            ScoreContainer.SetActive(PaperPlanesData.IsRunning);
            ScoreText.text = GetScoreText();
        }
    }
}