using Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    None,
    MainMenu,
    Navigation,
    Minigame,
    Ending,
    NavigationDemo,
}

public enum MinigameName
{
    None,
    SlidingPuzzle,
    Mastermind,
    WhereIsWaldo,
    Hacking,
    SimonSays,
    Hangman,
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
        DataManager.SelectedMinigame = minigameName;
        LoadScene(GameSceneToId(GameScene.Minigame));
    }

    /// <summary>
    /// Convert the GameScene enum to an integer. Used to load scenes. Default is MainMenu.
    /// </summary>
    /// <param name="gameScene"></param>
    /// <returns></returns>
    public static int GameSceneToId(GameScene gameScene) => gameScene switch
    {
        GameScene.MainMenu => 0,
        GameScene.Navigation => 4,
        GameScene.Minigame => 2,
        GameScene.Ending => 3,
        GameScene.NavigationDemo => 4,
        _ => 0,
    };
}