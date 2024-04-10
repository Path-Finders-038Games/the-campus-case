using Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
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
    Hangman
}

public class SceneLoader : MonoBehaviour
{
    public GameScene GameScene;
    public MinigameName SelectedMinigame;

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
        DataManager.CurrentMap = GetMinigameMapName(minigameName);
        LoadScene(GameSceneToId(GameScene.Minigame));
    }
    
    public static string GetMinigameMapName(MinigameName minigameName) => minigameName switch
    {
        //MinigameName.SlidingPuzzle => "C0Map",
        MinigameName.Mastermind => "S0Map",
        MinigameName.WhereIsWaldo => "X1Map",
        MinigameName.Hacking => "T2Map",
        MinigameName.SimonSays => "T5Map",
        MinigameName.Hangman => "C0Map",
        _ => "C0Map",
    };

    /// <summary>
    /// Convert the GameScene enum to an integer. Used to load scenes. Default is MainMenu.
    /// </summary>
    /// <param name="gameScene"></param>
    /// <returns></returns>
    public static int GameSceneToId(GameScene gameScene) => gameScene switch
    {
        GameScene.MainMenu => 0,
        GameScene.Navigation => 1,
        GameScene.Minigame => 2,
        GameScene.Ending => 3,
        _ => 0,
    };
}