using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    MainMenu,
    Navigation,
    Minigame,
    Ending,
}

public class SceneLoader : MonoBehaviour
{
    public GameScene GameScene;

    public void LoadScene()
    {
        LoadScene(GameSceneToId(GameScene));
    }

    public static void LoadScene(GameScene gameScene)
    {
        LoadScene(GameSceneToId(gameScene));
    }

    public static void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    /// <summary>
    /// Convert the GameScene enum to an integer. Used to load scenes. Default is MainMenu.
    /// </summary>
    /// <param name="gameScene"></param>
    /// <returns></returns>
    private static int GameSceneToId(GameScene gameScene) => gameScene switch
    {
        GameScene.MainMenu => 0,
        GameScene.Navigation => 1,
        GameScene.Minigame => 2,
        GameScene.Ending => 3,
        _ => 0,
    };
}