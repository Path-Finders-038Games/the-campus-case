using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.WaldoMinigame
{
    public class WaldoMinigame : Minigame
    {
        public GameObject Target;
        public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        public GameObject GameBoard;
        public Button HideLocationFileButton;
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;

        private bool _isChecking;

        private List<Dialogue> _startMinigame = new();
        private List<Dialogue> _endMinigame = new();

        void Start()
        {
            GameSetup();
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
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_0"));
            _startMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_1"));
            _endMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "waldoMinigame_2"));
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
        /// Set the location file for the minigame based on the language.
        /// </summary>
        public override void SetLocationFile()
        {
            LocationFile = PlayerPrefs.GetString("Language").Equals("NL") ? DutchFile() : EnglishFile();

            LocationUIName.text = LocationFile.Name;
            LocationUIDescription.text = LocationFile.Description;

            StringBuilder locationFacts = new("Facts\n");
            foreach (string fact in LocationFile.Facts)
            {
                locationFacts.AppendLine(fact + "\n");
            }

            LocationUIFacts.text = locationFacts.ToString();
        }

        /// <summary>
        /// Return the Dutch location file.
        /// </summary>
        /// <returns>Dutch <see cref="LocationFile"/>.</returns>
        public override LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description,
                LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);
        }

        /// <summary>
        /// Return the English location file.
        /// </summary>
        /// <returns>English <see cref="LocationFile"/>.</returns>
        public override LocationFile EnglishFile()
        {
            return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description,
                LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);
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

        /// <summary>
        /// Sets the buddy image based on the player's choice.
        /// </summary>
        public override void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            BuddyImage.GetComponent<Image>().sprite = buddyChoice switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite,
            };
        }

        /// <summary>
        /// Set the buddy dialogue text in the UI.
        /// </summary>
        /// <param name="dialogue">Text to display in the dialog bubble.</param>
        public override void SetBuddyDialogueText(string dialogue)
        {
            BuddyTextBlock.text = dialogue;
            BuddyDialogueObject.SetActive(true);
        }

        /// <summary>
        /// Handle the text block click event. Set the dialogue as read and hide the dialog bubble.
        /// </summary>
        public override void OnTextBlockClick()
        {
            string text = BuddyTextBlock.text;

            if (!LocationFile.IsCompleted)
            {
                _startMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }
            else
            {
                _endMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }

            BuddyDialogueObject.SetActive(false);
        }

        /// <summary>
        /// Update the buddy dialogue based on the minigame progress.
        /// </summary>
        public override void UpdateDialogue()
        {
            if (!LocationFile.IsCompleted)
            {
                Dialogue dialogue = _startMinigame.First(dialogue => dialogue.IsRead != true);
                SetBuddyDialogueText(dialogue.Text);
            }
            else
            {
                Dialogue dialogue = _endMinigame.First(dialogue => dialogue.IsRead != true);
                SetBuddyDialogueText(dialogue.Text);
            }
        }
    }
}