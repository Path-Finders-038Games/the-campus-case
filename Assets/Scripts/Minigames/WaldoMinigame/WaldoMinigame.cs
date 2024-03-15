using Dialog;
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


        void Start()
        {
            SetBuddy();
            SplitDialogue();
            PrepareStep();
            StartGameStep();
        }

        void Update()
        {
            UpdateDialogue();

            if (!_isChecking) return;

            Debug.Log(Input.touchCount);

            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
        }

        /// <summary>
        /// Split the dialogue into the start and end of the minigame.
        /// TODO: Replace with localized dialogue.
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
            if (LocationFile.IsCompleted)
            {
                SceneManager.LoadScene(1);
            }
        }

        /// <summary>
        /// Check if the raycast hits the target. If so, complete the minigame.
        /// </summary>
        private void RaycastCheck()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (!Physics.Raycast(ray, out hit)) return;

            string name = hit.transform.gameObject.name;
            if (name.Equals(Target.name)) CompleteGameStep();
        }
    }
}