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
        public Button HideLocationFileButton;

        void Start()
        {
            SetBuddy();
            SplitDialogue();
            PrepareStep();
            StartGameStep();
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
            StartMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_0"));
            StartMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_1"));
            EndMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_2"));
        }
    }
}