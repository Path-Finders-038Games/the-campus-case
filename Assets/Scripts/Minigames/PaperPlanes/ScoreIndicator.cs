using System;
using TMPro;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class ScoreIndicator : MonoBehaviour
    {
        public TMP_Text ScoreText;
        
        private static string GetScoreText()
        {
            return $"Planes Spawned: {PaperPlanesData.PlanesSpawned}\n" +
                   $"Planes Hit: {PaperPlanesData.PlanesHit}\n" +
                   $"Planes Missed: {PaperPlanesData.PlanesMissed}";
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
            ScoreText.text = GetScoreText();
        }
    }
}