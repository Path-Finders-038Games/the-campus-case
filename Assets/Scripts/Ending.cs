using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public RawImage image;
    public List<Texture2D> TextureList;
    int currentImage;
    // Start is called before the first frame update
    void Start()
    {
        currentImage = 0;
        image.texture = TextureList[currentImage];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            currentImage++;
           

            if (currentImage >= TextureList.Count)
            {
                SceneManager.LoadScene(0);
            }

            image.texture = TextureList[currentImage];
        }
    }
}
