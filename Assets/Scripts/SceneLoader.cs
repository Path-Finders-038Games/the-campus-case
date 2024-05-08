using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    None,
    MainMenu,
    Navigation,
    Minigame,
    Ending,
}

public enum MinigameName
{
    None,
    SlidingPuzzle,
    Mastermind,
    WhereIsWaldo,
    Hacking,
    SimonSays,
    PaperPlanes,
    // Hangman,
}

public class SceneLoader : MonoBehaviour
{
    public GameScene GameScene = GameScene.None;
    public MinigameName SelectedMinigame = MinigameName.None;

    public void LoadScene()
    {
        LoadScene(GameSceneToId(GameScene));
    }

    public static void LoadScene(GameScene gameScene)
    {
        LoadScene(GameSceneToId(gameScene));
        
        // unload all other scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex != GameSceneToId(gameScene))
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    private static void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadMinigame()
    {
        LoadMinigame(SelectedMinigame);
    }

    public static void LoadMinigame(MinigameName minigameName)
    {
        try
        {
            DataManager.CameraPosition = Camera.main.transform.position;
        }
        catch
        {
            Debug.LogWarning("Camera \"MainCamera\" not found.");
        }

        DataManager.SelectedMinigame = minigameName;
        LoadScene(GameSceneToId(GameScene.Minigame));
    }

    /// <summary>
    /// Convert the GameScene enum to an integer. Used to load scenes. Default is MainMenu.
    /// </summary>
    /// <param name="gameScene">GameScene to switch to.</param>
    /// <returns>Id of the scene.</returns>
    public static int GameSceneToId(GameScene gameScene) => gameScene switch
    {
        GameScene.MainMenu => 0,
        GameScene.Navigation => !DataManager.DemoMode ? 1 : 4, // Demo mode uses a different navigation scene. (T5 map)
        GameScene.Minigame => 2,
        GameScene.Ending => 3,
        _ => 0,
    };
}