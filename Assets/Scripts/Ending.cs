using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public RawImage image;
    public List<Texture2D> TextureList;
    public List<Texture2D> TextureListDemo;

    /// <summary>
    /// Gets the texture list based on the game mode.
    /// </summary>
    /// <returns>List with all 2D textures.</returns>
    private List<Texture2D> GetTextureList() => DataManager.DemoMode ? TextureListDemo : TextureList;
    
    private int _currentImage;

    /// <summary>
    /// Inits the ending cutscene.
    /// </summary>
    private void Start()
    {
        _currentImage = 0;
        image.texture = GetTextureList()[_currentImage];
    }

    /// <summary>
    /// Goes to the next image in the cutscene when the screen is touched.
    /// </summary>
    private void Update()
    {
        if (Input.touchCount < 1 || Input.touches[0].phase != TouchPhase.Began) return;

        _currentImage++;

        if (_currentImage >= GetTextureList().Count)
        {
            SceneLoader.LoadScene(GameScene.MainMenu);
        }

        image.texture = GetTextureList()[_currentImage];
    }
}