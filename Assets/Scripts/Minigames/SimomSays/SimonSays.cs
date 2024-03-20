using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//todo
//delete buttonmat
//remove buttons and use only the list
namespace Minigames.SimomSays
{
    public class SimonSays : Minigame
    {
        //Buttons

        // button on screen to start the simon says game
        public Button StartButton;

        //not used anywhere???
        public Material ButtonMat;

        //Safe buttons
        public GameObject LTButton;//0
        public GameObject RTButton;//1
        public GameObject LBButton;//2
        public GameObject RBButton;//3

        //Text field for displaying messages
        public TMP_Text Text;


        //Start sequence lenght for round 1
        private int _lenghtSequence = 3;

        // list of the buttons on the safe
        private List<GameObject> _safeButtonList = new();

        //sequence of buttons to press
        private List<int> _taskSequence = new();

        //sequence of buttons the player presses
        private List<int> _playerSequence = new();

        //rounds the game will play
        private int _rounds = 3;

        //current round of the game
        private int _curentRound = 1;

        //delay duration used to slow down game elements
        private int _waitTime = 1;

        //boolean used to control whether player input should be registered
        private bool _isDoneChecking = true;

        //bunch of likely deprecated central minigame class stuff
        public TMP_Text LocationUIName; 
        public TMP_Text LocationUIDescription; 
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        public LocationInfoScriptableObject LocationInfo;

        // variable used for the AR interaction unsure of its exact behaviour
        private int _raycastRange = 100;

        //boolean that signifies whether the player is still in play or has either lost or hasnt started yet
        private bool _isPlaying = false;

        //button for hiding location file?
        //probably not used (yet) 
        public Button HideLocationFileButton;

        //text block where your buddy talks to you
        public TMP_Text BuddyTextBlock;

        //main object under which the buddy objects are placed
        //used to easily active and deactivate the buddy
        public GameObject BuddyDialogueObject;

        //image for the buddy
        public GameObject BuddyImage;

        //buddy if its a dog
        public Sprite BuddyDogSprite;

        //buddy if its a cat
        public Sprite BuddyCatSprite;

        //the dialogue for the game
        private List<Dialogue> _startMinigame = new();
        // Start is called before the first frame update
        void Start()
        {
            GameSetup();
        }
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

        //???
        public override void SplitDialogue()
        {
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_0"));
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "simonSaysMinigame_1"));
        }

        // setup method to configure the minigame before it starts
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

        // method for retrieving info based on language
        public override void SetLocationFile()
        {
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                LocationFile = DutchFile();
            }
            else
            {
                LocationFile = EnglishFile();
            }
            LocationUIName.text = LocationFile.Name;
            LocationUIDescription.text = LocationFile.Description;
            StringBuilder locationFacts = new("Facts\n");
            foreach (string fact in LocationFile.Facts)
            {
                locationFacts.AppendLine(fact + "\n");
            }
            LocationUIFacts.text = locationFacts.ToString();
        }

        // retrieve info for dutch
        public override LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description, LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);

        }

        //retrieve info for english
        public override LocationFile EnglishFile()
        {
            return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description, LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);

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
                int randomButtonId = Random.Range(0, _safeButtonList.Count);
                _taskSequence.Add(randomButtonId);
            }
        }
        //expands on the basic sequence each round
        private void AddToSequence()
        {
            int randomButtonId = Random.Range(0, _safeButtonList.Count);
            _taskSequence.Add(randomButtonId);
        }

        //lights up a button
        private void LightUpButton(int buttonId)
        {
            _safeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 1);
        }

        //darkens the button
        private void LightOffButton(int buttonId)
        {
            _safeButtonList[buttonId].GetComponent<MeshRenderer>().material.SetFloat("_" + buttonId.ToString(), 0);
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
                LightUpButton(buttonId);
                yield return new WaitForSeconds(_waitTime);
                LightOffButton(buttonId);
            }
            Text.text = "Your Turn";
            yield return new WaitForSeconds(_waitTime);
            Text.text = $"Round: {_curentRound}";
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
        //-------------------------------------------------------------
        // likely deprecated
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


        public override void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            if (buddyChoice.Equals("Cat"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyCatSprite;
            }
            if (buddyChoice.Equals("Dog"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyDogSprite;
            }
        }
        public override void SetBuddyDialogueText(string Dialogue)
        {
            BuddyTextBlock.text = Dialogue;
            BuddyDialogueObject.SetActive(true);
        }
        public override void OnTextBlockClick()
        {
            string text = BuddyTextBlock.text;
            if (!LocationFile.IsCompleted)
            {
                _startMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }
            BuddyDialogueObject.SetActive(false);
        }
        public override void UpdateDialogue()
        {
            if (!LocationFile.IsCompleted)
            {
                foreach (Dialogue dialogue in _startMinigame)
                {
                    if (dialogue.IsRead != true)
                    {
                        SetBuddyDialogueText(dialogue.Text);
                        break;
                    }
                }
            }

        }
    }
}
