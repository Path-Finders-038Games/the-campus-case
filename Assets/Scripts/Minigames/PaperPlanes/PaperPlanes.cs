using Dialog;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class PaperPlanes : Minigame
    {
        private void Update()
        {
            UpdateDialogue();
            
            if (PaperPlanesData.PlanesHit >= PaperPlanesData.WIN_SCORE)
            {
                DataManager.SetMinigameStatus(MinigameName.PaperPlanes, true);
                GameState = GameState.Won;
                
                LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
                ShowLocationFile();
            }
            else if (PaperPlanesData.PlanesMissed >= PaperPlanesData.LOSE_SCORE)
            {
                GameState = GameState.Lost;
                
                LocationUIHintNextLocation.text = "Better luck next time!";
                ShowLocationFile();
            }
        }

        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "paperPlanesMinigame_0"));
        }

        /// <summary>
        /// Reset the data for the minigame
        /// </summary>
        public override void PrepareStep()
        {
            PaperPlanesData.ResetData();
        }

        public override void StartGameStep()
        {
            // Not needed, the game is started when the minigame is loaded
        }

        public override void CompleteGameStep()
        {
            // Not needed, the game is completed when the win condition is met
        }

        // shows location file
        public override void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }

        //hides location file and returns to the map
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);

            if (LocationFile.IsCompleted)
            {
                // 2nd time this is called, return to map
                SceneLoader.LoadScene(GameScene.Navigation);
            }
            else
            {
                // 1st time this is called, set location file as completed and start the game
                LocationFile.IsCompleted = true;
                PaperPlanesData.IsRunning = true;
            }
        }
    }
}