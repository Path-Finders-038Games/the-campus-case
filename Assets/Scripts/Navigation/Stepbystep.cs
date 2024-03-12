using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class Stepbystep : MonoBehaviour
    {
        public TMP_Text GuideText;
        public Image GuideImage;

        /// <summary>
        /// Updates the guide with the given text and image.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="image">Image to accompany the text.</param>
        public void UpdateGuide(string text, Sprite image)
        {
            GuideText.text = text;
            GuideImage.sprite = image;
        }
    }
}