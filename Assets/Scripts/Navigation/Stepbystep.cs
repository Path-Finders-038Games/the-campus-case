using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class Stepbystep : MonoBehaviour
    {
        public TMP_Text GuideText;
        public Image GuideImage;
   
        public void UpdateGuide(string text, Sprite image)
        {
            GuideText.text = text;
            GuideImage.sprite = image;
        }
    }
}
