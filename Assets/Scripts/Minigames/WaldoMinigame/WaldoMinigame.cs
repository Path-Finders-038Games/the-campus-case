using Dialog;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.WaldoMinigame
{
    public class WaldoMinigame : Minigame
    {
        public GameObject Target;
        public GameObject GameBoard;
        public Button HideLocationFileButton;

        private bool _isChecking;

        protected override void Start()
        {
            base.Start();
            
            _isChecking = false;
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
            ShowLocationFile();
        }

        private void Update()
        {
            UpdateDialogue();

            if (!_isChecking) return;
            if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;

            RayCastCheck();
        }

        /// <summary>
        /// Split the dialogue into the start and end of the minigame.
        /// </summary>
        protected override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_1"));
            WonDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_2"));
        }

        /// <summary>
        /// Complete the minigame.
        /// </summary>
        protected override void HandleGameOver()
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
            base.ShowLocationFile();
            
            _isChecking = false;
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
            
            HandleGameOver();
        }
    }
}