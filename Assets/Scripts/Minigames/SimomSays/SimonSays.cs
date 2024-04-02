using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.SimomSays
{
    public class SimonSays : Minigame
    {
        // button on screen to start the simon says game
        public Button StartButton;
        //Text field for displaying messages
        public TMP_Text Text;
        // list of the buttons on the safe
        public List<GameObject> SafeButtonList = new();
        //button for hiding location file
        public Button HideLocationFileButton;

        //Start sequence lenght for round 1
        private int _lenghtSequence = 3;
        //sequence of buttons to press
        private List<int> _taskSequence = new();
        //sequence of buttons the player presses
        private List<int> _playerSequence = new();
        //rounds the game will play
        private int _rounds = 3;
        //current round of the game
        private int _currentRound = 1;
        //delay duration used to slow down game elements
        private int _waitTime = 1;
        //boolean used to control whether player input should be registered
        private bool _isDoneChecking = true;
        // variable used for the AR interaction unsure of its exact behaviour
        private int _raycastRange = 100;
        //boolean that signifies whether the player is still in play or has either lost or hasnt started yet
        private bool _isPlaying = false;
        //the dialogue for the game
        private List<Dialogue> _startMinigame = new();

        void Update()
        {
            UpdateDialogue();

            //if player input should be registered does something
            if (_isDoneChecking == false)
            {
                //checks if the player touched their screen
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    RaycastCheck();
                }
            }
            //if the user isnt playing yet allow them to interact with the start button
            if (!_isPlaying)
            {
                StartButton.interactable = true;
            }
        }

        //adds dialoguetext to the minigame from the localizationtable
        public override void SplitDialogue()
        {
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_0"));
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_1"));
        }

        // setup method to configure the minigame before it starts
        public override void PrepareStep()
        {
            //Add listeners to safe buttons
            StartButton.onClick.AddListener(StartGame);
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
            SetLocationFile();
            StartButton.interactable = false;
        }

        // start of the minigame setup procedure
        public override void StartGameStep()
        {
            ShowLocationFile();
        }

        // method for configuring the minigame as completed by the player
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

        //Makes the basic start sequence
        private void MakeSequence()
        {
            for (int i = 0; i < _lenghtSequence; i++)
            {
                int randomButtonId = Random.Range(0, SafeButtonList.Count);
                _taskSequence.Add(randomButtonId);
            }
        }
        //expands on the basic sequence each round
        private void AddToSequence()
        {
            int randomButtonId = Random.Range(0, SafeButtonList.Count);
            _taskSequence.Add(randomButtonId);
        }

        //lights up a button
        private void LightUpButton(int buttonId)
        {
            SafeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 1);
        }

        //darkens the button
        private void LightOffButton(int buttonId)
        {
            SafeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 0);
        }

        //brightens and darkens the button
        private IEnumerator LightUpAndOffButton(int buttonId)
        {
            LightUpButton(buttonId);
            yield return new WaitForSeconds(_waitTime);
            LightOffButton(buttonId);
        }

        //Shows the button sequence to the player
        private IEnumerator ShowSequence()
        {
            //Shows the sequence
            foreach (int buttonId in _taskSequence)
            {
                yield return new WaitForSeconds(_waitTime);
                Text.text = "Showing Sequence";
                StartCoroutine(LightUpAndOffButton(buttonId));
            }
            Text.text = "Your Turn";
            yield return new WaitForSeconds(_waitTime);
            Text.text = $"Round: {_currentRound}";
            _isDoneChecking = false;
        }

        //Add playerinput button to List and check if player fails,continues or wins
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

            //if the current playersequence matches the games sequence proceed to the next round
            if (_playerSequence.Count == _taskSequence.Count)
            {
                Text.text = "Won";
                NextRound();
            }
        }
        //Resets game
        private void ResetGame()
        {
            _currentRound = 1;
            _playerSequence.Clear();
            _taskSequence.Clear();
        }
        //prepares next round
        private void NextRound()
        {
            _playerSequence.Clear();
            if (_currentRound == _rounds)
            {
                Text.text = "You won the game";
                CompleteGameStep();
            }
            else
            {
                _currentRound++;
                AddToSequence();
                StartCoroutine(ShowSequence());
            }
        }


        //Checks what the player has touched with a raycast
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
        //what happens when a button is pressed
        private void SimonButtonEvent(int buttonId)
        {
            StartCoroutine(LightUpAndOffButton(buttonId));
            AddToPlayerTaskSequence(buttonId);
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
            StartButton.interactable = true;
            if (LocationFile.IsCompleted)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
