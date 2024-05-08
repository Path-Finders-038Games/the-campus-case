using Dialog;

namespace Minigames.PaperPlanes
{
    public class PaperPlanes : Minigame
    {
        /// <summary>
        /// Resets the data for the paper planes minigame.
        /// </summary>
        private void Start()
        {
            base.Start();
            
            PaperPlanesData.ResetData();
        }
        
        /// <summary>
        /// Updates the dialogue based on the game state, as well as check for win/lose conditions.
        /// </summary>
        private void Update()
        {
            UpdateDialogue();

            if (PaperPlanesData.GameOver)
            {
                HandleGameOver();
            }
        }

        /// <summary>
        /// Splits the dialogue into the tutorial dialogues.
        /// </summary>
        protected override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "paperPlanesMinigame_0"));
        }

        protected override void HandleGameOver()
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