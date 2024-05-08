using Dialog;

namespace Minigames.PaperPlanes
{
    public class PaperPlanes : Minigame
    {
        /// <summary>
        /// Updates the dialogue based on the game state, as well as check for win/lose conditions.
        /// </summary>
        private void Update()
        {
            UpdateDialogue();

            if (PaperPlanesData.GameOver)
            {
                CompleteGameStep();
            }
        }

        /// <summary>
        /// Prepares the minigame step. Resets the data for the minigame.
        /// </summary>
        public override void PrepareStep()
        {
            PaperPlanesData.ResetData();
        }

        /// <summary>
        /// Splits the dialogue into the tutorial dialogues.
        /// </summary>
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "paperPlanesMinigame_0"));
        }

        public override void StartGameStep()
        {
            // Not needed, the game is started when the minigame is loaded
        }

        public override void CompleteGameStep()
        {
            if (PaperPlanesData.PlanesHit >= PaperPlanesData.WinScore)
            {
                DataManager.SetMinigameStatus(MinigameName.PaperPlanes, true);
                GameState = GameState.Won;

                LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            }
            else if (PaperPlanesData.PlanesMissed >= PaperPlanesData.LoseScore)
            {
                GameState = GameState.Lost;

                LocationUIHintNextLocation.text = "Better luck next time!";
            }

            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }

        /// <summary>
        /// Shows the location file.
        /// </summary>
        public override void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }

        /// <summary>
        /// Hides the location file and returns to the map if the file is completed.
        /// </summary>
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);

            if (LocationFile.IsCompleted)
            {
                // 2nd time this is called, return to map
                SceneLoader.LoadScene(GameScene.Navigation);
                return;
            }

            PaperPlanesData.IsRunning = true;
        }
    }
}