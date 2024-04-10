using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public RawImage image;
    public List<Texture2D> TextureList;

    private int _currentImage;

    /// <summary>
    /// Inits the ending cutscene.
    /// </summary>
    void Start()
    {
        _currentImage = 0;
        image.texture = TextureList[_currentImage];
    }

    /// <summary>
    /// Goes to the next image in the cutscene when the screen is touched.
    /// </summary>
    void Update()
    {
        if (Input.touchCount < 1 || Input.touches[0].phase != TouchPhase.Began) return;

        _currentImage++;

        if (_currentImage >= TextureList.Count)
        {
            SceneLoader.LoadScene(GameScene.MainMenu);
        }

        image.texture = TextureList[_currentImage];
    }
}