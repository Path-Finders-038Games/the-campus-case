using Dialog;
using Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.WaldoMinigame
{
    public class WaldoMinigame : Minigame
    {
        public GameObject Target;
        public GameObject GameBoard;
        public Button HideLocationFileButton;

        private bool _isChecking;

        void Update()
        {
            UpdateDialogue();

            if (!_isChecking) return;
            if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;

            RayCastCheck();
        }

        /// <summary>
        /// Split the dialogue into the start and end of the minigame.
        /// </summary>
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_1"));
            WonDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_2"));
        }

        /// <summary>
        /// Prepare the minigame.
        /// </summary>
        public override void PrepareStep()
        {
            _isChecking = false;
            SetLocationFile();
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
        }

        /// <summary>
        /// Start the minigame.
        /// </summary>
        public override void StartGameStep()
        {
            ShowLocationFile();
        }

        /// <summary>
        /// Complete the minigame.
        /// </summary>
        public override void CompleteGameStep()
        {
            DataManager.SetMinigameStatus(MinigameName.WhereIsWaldo, true);
            
            _isChecking = false;
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
            GameBoard.SetActive(false);
        }

        /// <summary>
        /// Show the location file for the minigame in the UI.
        /// </summary>
        public override void ShowLocationFile()
        {
            _isChecking = false;
            LocationFileUI.SetActive(true);
        }

        /// <summary>
        /// Hide the location file for the minigame in the UI.
        /// </summary>
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            _isChecking = true;

            if (!LocationFile.IsCompleted) return;

            SceneLoader.LoadScene(GameScene.Navigation);
        }

        /// <summary>
        /// Check if the ray cast hits the target. If so, complete the minigame.
        /// Target is the hidden object in the minigame.
        /// </summary>
        private void RayCastCheck()
        {
            if (!Camera.main) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            if (!hit.transform.gameObject.name.Equals(Target.name)) return;
            
            CompleteGameStep();
        }
    }
}