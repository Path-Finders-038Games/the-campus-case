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
        private List<GameObject> _safeButtonList = new List<GameObject>();
        private List<int> _taskSequence = new List<int>();
        private List<int> _playerSequence = new List<int>();
        private int _rounds = 3;
        private int _curentRound = 1;
        private int _waitTime = 1;
        private bool _isDoneChecking = true;
        public TMP_Text LocationUIName; 
        public TMP_Text LocationUIDescription; 
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        public LocationInfoScriptableObject LocationInfo;
        private int _raycastRange = 100;
        private bool _isPlaying = false;
        public Button HideLocationFileButton;
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;
        private List<Dialogue> _startMinigame = new List<Dialogue>();
        // Start is called before the first frame update
        void Start()
        {
            GameSetup();
        }
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
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-5][0]);
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-5][1]);
            }
            if (PlayerPrefs.GetString("Language").Equals("EN"))
            {
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-5][0]);
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-5][1]);
            }
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
            StringBuilder locationFacts = new StringBuilder("Facts\n");
            foreach (string fact in LocationFile.Facts)
            {
                locationFacts.AppendLine(fact + "\n");
            }
            LocationUIFacts.text = locationFacts.ToString();
        }

        public override LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description, LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);

        }

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
