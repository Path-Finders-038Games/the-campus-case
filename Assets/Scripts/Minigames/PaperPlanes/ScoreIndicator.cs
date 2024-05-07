using System;
using TMPro;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class ScoreIndicator : MonoBehaviour
    {
        public TMP_Text ScoreText;
        public GameObject ScoreContainer;

        private static string GetScoreText()
        {
            return $"Planes Hit: {PaperPlanesData.PlanesHit}/{PaperPlanesData.WIN_SCORE}\n" +
                   $"Planes Missed: {PaperPlanesData.PlanesMissed}/{PaperPlanesData.LOSE_SCORE}";
        }

        private void Start()
        {
            if (ScoreText == null)
            {
                throw new ArgumentNullException(nameof(ScoreText), "ScoreText is not set in the Unity Editor.");
            }
        }

        private void Update()
        {
            ScoreContainer.SetActive(PaperPlanesData.IsRunning);
            ScoreText.text = GetScoreText();
        }
    }
}