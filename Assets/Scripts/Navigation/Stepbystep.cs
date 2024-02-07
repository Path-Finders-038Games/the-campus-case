using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
