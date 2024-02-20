using System.Collections.Generic;
using System.Text;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.Mastermind
{
    public class Manager : Minigame
    {
        [SerializeField] Mastermind mastermind;
        [SerializeField] GameObject[] gameSlot =  new GameObject[10];
        [SerializeField] GameObject[] gameVerif = new GameObject[4];
        private int _currentSlot, _currentCol;
        private Sprite _emptySprite;
        [SerializeField] string[] code;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        public Button HideLocationFileButton;
        public LocationInfoScriptableObject LocationInfo;

        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;
        private List<Dialogue> _startMinigame = new();
        private List<Dialogue> _endMinigame = new();

        void Start()
        {
            GameSetup();
        }
        private void Update()
        {
            UpdateDialogue();
        }

        public void ColorSelect(Sprite sp)
        {

            if (!mastermind.HiddenSlot.activeInHierarchy) return;

            gameSlot[_currentSlot].transform.Find("c" + _currentCol).GetComponent<Image>().sprite = sp;
            code.SetValue(sp.name, _currentCol - 1);
            _currentCol++;
            if (_currentCol == 5) _currentCol = 1;
        }

        public void Cancel()
        {
            for (int i = 1; i < 5; i++)
            {
                gameSlot[_currentSlot].transform.Find("c" + i).GetComponent<Image>().sprite = _emptySprite;

            }
            _currentCol = 1;
        }

        public void Replay()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        public void ExitGame()
        {
            SceneManager.LoadScene(1);
        }

        public void Check()
        {
            if (!mastermind.HiddenSlot.activeInHierarchy) return;
            int x = 0;
            foreach (Transform child in gameSlot[_currentSlot].transform)
            {
                if (child.tag == "Verif")
                {
                    gameVerif[x] = child.gameObject;
                    x++;
                }
            }

            for (int i = 1; i < 5; i++)
            {
                if (gameSlot[_currentSlot].transform.Find("c" + i).GetComponent<Image>().sprite == _emptySprite) return;

            }


            int nbGoodPosition = mastermind.GetGoodPosition(code);
            for (int i = 0; i < nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.Black;
            }

            int nbWrongPosition = mastermind.GetWrongPosition();
            for (int i = nbGoodPosition; i < nbWrongPosition + nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.White;
            }

            if (nbGoodPosition == 4)
            {
                CompleteGameStep();
                return;
            }

            if (_currentSlot == 9)
            {
                Loose();
                return;

            }

            _currentSlot++;

            Color originColor = gameSlot[_currentSlot].GetComponent<Image>().color;
            Color selectionColor = originColor;
            selectionColor.a = 0.25f;

            gameSlot[_currentSlot].GetComponent<Image>().color = selectionColor;
            gameSlot[_currentSlot - 1].GetComponent<Image>().color = originColor;
        }

        void Win()

        {
            mastermind.HiddenSlot.SetActive(false);
        }


        void Loose()

        {
            mastermind.HiddenSlot.SetActive(false);
        }

        public override void PrepareStep()
        {
            _currentSlot = 0;
            _currentCol = 1;
            code = new string[4];
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
            SetLocationFile();
        }

        public override void StartGameStep()
        {
            mastermind.GetNewSecretCode();
            _emptySprite = gameSlot[_currentSlot].transform.Find("c1").GetComponent<Image>().sprite;
            ShowLocationFile();
        }

        public override void CompleteGameStep()
        {
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
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
            StringBuilder locationFacts = new("Facts\n");
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
        public override void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            if (LocationFile.IsCompleted)
            {
                SceneManager.LoadScene(1);
            }

        }
        public override void SplitDialogue()
        {
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-2][0]);
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-2][1]);
                _endMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-2][2]);
            }
            if (PlayerPrefs.GetString("Language").Equals("EN"))
            {
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-2][0]);
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-2][1]);
                _endMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-2][2]);
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
            else
            {
                _endMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
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
            else
            {
                foreach (Dialogue dialogue in _endMinigame)
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