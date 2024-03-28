using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadMainMenuOnPlayInEditor : EditorWindow
{
    private const GameScene StartGameScene = GameScene.MainMenu;

    private void OnGUI()
    {
        EditorSceneManager.playModeStartScene =
            AssetDatabase.LoadAssetAtPath<SceneAsset>($"Assets/Scenes/{StartGameScene}.unity");
    }
}