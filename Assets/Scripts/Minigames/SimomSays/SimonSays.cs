using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.SimomSays
{
    public class SimonSays : Minigame
    {
        //Buttons
        public Button StartButton;
        public Material ButtonMat;
        //Safe buttons
        public GameObject LTButton;//0
        public GameObject RTButton;//1
        public GameObject LBButton;//2
        public GameObject RBButton;//3
        //Text field
        public TMP_Text Text;
        //Start sequence lenght
        private int _lenghtSequence = 3;
        private List<GameObject> _safeButtonList = new();
        private List<int> _taskSequence = new();
        private List<int> _playerSequence = new();
        private int _rounds = 3;
        private int _curentRound = 1;
        private int _waitTime = 1;
        private bool _isDoneChecking = true;
        private int _raycastRange = 100;
        private bool _isPlaying = false;
        public Button HideLocationFileButton;
        
        void Update()
        {
            UpdateDialogue();
            if (_isDoneChecking == false)
            {
                //Screen touched check
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    RaycastCheck();
                }
            }
            if (!_isPlaying)
            {
                StartButton.interactable = true;
            }
        }
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_1"));
        }
        public override void PrepareStep()
        {
            //Add safe buttons to list
            _safeButtonList.Add(LTButton);
            _safeButtonList.Add(RTButton);
            _safeButtonList.Add(LBButton);
            _safeButtonList.Add(RBButton);
            //Add listeners to safe buttons
            StartButton.onClick.AddListener(StartGame);
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
            SetLocationFile();
            StartButton.interactable = false;
        }
        public override void StartGameStep()
        {
            ShowLocationFile();
        }
        public override void CompleteGameStep()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetTrigger("ToggleOpen");
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
        }
        //starts the game
        private void StartGame()
        {
            _isDoneChecking = true;
            _isPlaying = true;
            StartButton.interactable = false;
            ResetGame();
            MakeSequence();
            StartCoroutine(ShowSequence());
        }
        //Makes the start sequence
        private void MakeSequence()
        {
            for (int i = 0; i < _lenghtSequence; i++)
            {
                int randomButtonId = Random.Range(0, _safeButtonList.Count);
                _taskSequence.Add(randomButtonId);
            }
        }
        //Adds to the sequence
        private void AddToSequence()
        {
            int randomButtonId = Random.Range(0, _safeButtonList.Count);
            _taskSequence.Add(randomButtonId);
        }
        private void LightUpButton(int buttonId)
        {
            _safeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 1);
        }
        private void LightOffButton(int buttonId)
        {
            _safeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 0);
        }
        private IEnumerator LightUpAndOffButton(int buttonId)
        {
            LightUpButton(buttonId);
            yield return new WaitForSeconds(_waitTime);
            LightOffButton(buttonId);
        }
        //Shows the sequence
        private IEnumerator ShowSequence()
        {
            //Shows the sequence
            foreach (int buttonId in _taskSequence)
            {
                yield return new WaitForSeconds(_waitTime);
                Text.text = "Showing Sequence";
                LightUpButton(buttonId);
                yield return new WaitForSeconds(_waitTime);
                LightOffButton(buttonId);
            }
            Text.text = "Your Turn";
            yield return new WaitForSeconds(_waitTime);
            Text.text = $"Round: {_curentRound}";
            _isDoneChecking = false;
        }
        //Add player buttons to List and check if player fails,continues or wins
        private void AddToPlayerTaskSequence(int buttonId)
        {
            _playerSequence.Add(buttonId);
            for (int index = 0; index < _playerSequence.Count; index++)
            {
                if (_playerSequence[index] == _taskSequence[index])
                {
                    continue;
                }
                else
                {
                    Text.text = "Lost";
                    ResetGame();
                    _isDoneChecking = true;
                    _isPlaying = false;
                    return;
                }
            }
            if (_playerSequence.Count == _taskSequence.Count)
            {
                Text.text = "Won";
                NextRound();
            }
        }
        //Resets game
        private void ResetGame()
        {
            _curentRound = 1;
            _playerSequence.Clear();
            _taskSequence.Clear();
        }
        //prepares next round
        private void NextRound()
        {
            _playerSequence.Clear();
            if (_curentRound == _rounds)
            {
                Text.text = "You won the game";
                CompleteGameStep();
            }
            else
            {
                _curentRound++;
                AddToSequence();
                StartCoroutine(ShowSequence());
            }
        }
        public override void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            StartButton.interactable = true;
            if (LocationFile.IsCompleted)
            {
                SceneManager.LoadScene(1);
            }
        }
        //Checks the raycast
        private void RaycastCheck() 
        {
            RaycastHit hit;
            Vector2 touchpos = Input.touches[0].position;
            Ray ray = Camera.main.ScreenPointToRay(touchpos);
            if (Physics.Raycast(ray, out hit, _raycastRange))
            {
                string name = hit.transform.gameObject.name;
                Debug.Log(name);
                switch (name)
                {
                    case "SafeButtonLT":
                        SimonButtonEvent(0);
                        break;
                    case "SafeButtonRT":
                        SimonButtonEvent(1);
                        break;
                    case "SafeButtonLB":
                        SimonButtonEvent(2);
                        break;
                    case "SafeButtonRB":
                        SimonButtonEvent(3);
                        break;
                    case "LocationFileCollectable":
                        ShowLocationFile();
                        break;
                    default:
                        _isDoneChecking = false;
                        break;
                }
            }
            else
            {
                _isDoneChecking = false;
            }
        }
        // Simon buttons eventhandlers
        private void SimonButtonEvent(int buttonId)
        {
            StartCoroutine(LightUpAndOffButton(buttonId));
            AddToPlayerTaskSequence(buttonId);
        }
    }
}
