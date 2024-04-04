using System;
using System.Collections.Generic;
using System.Linq;
using Dialog;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigames.Mastermind
{
    public class Mastermind : Minigame
    {
        [Header("Color Circle Sprites")] 
        public Sprite Star;
        public Sprite Moon;
        public Sprite Heart;
        public Sprite Square;
        public Sprite Circle;
        public Sprite Triangle;
        public Sprite Empty;

        [Header("Verification Sprites")] 
        public Sprite CorrectPosition;
        public Sprite CorrectColor;
        public Sprite Incorrect;

        [Header("Game Board Properties")] 
        public int MaxRows = 10;
        public int InitialRowY = -165;
        public int RowYOffset = -100;
        public int RowX = 400;
        public int RowZ = 0;
        private const int Cols = 4;

        [Header("Game Logic Variables")] 
        public GameObject RowParentPrefab;
        public GameObject RowPrefab;
        public GameObject HiddenCodeRow;
        private GameObject[] _boardRows;

        private int _currentRow, _currentCol;
        private string[] _playerInputCode;
        private readonly Dictionary<string, Sprite> _discoSprite = new(); // Dictionary to store the shape sprites
        private readonly string[] _secretCodeToGuess = new string[Cols];

        private void Update()
        {
            UpdateDialogue();
        }

        #region Converters

        private Sprite GetSpriteFromShape(MastermindShape mastermindShape) => mastermindShape switch
        {
            MastermindShape.Star => Star,
            MastermindShape.Moon => Moon,
            MastermindShape.Heart => Heart,
            MastermindShape.Square => Square,
            MastermindShape.Circle => Circle,
            MastermindShape.Triangle => Triangle,
            _ => null,
        };


        private static string MastermindColorToString(MastermindShape mastermindShape) => mastermindShape switch
        {
            MastermindShape.Star => "Star",
            MastermindShape.Moon => "Moon",
            MastermindShape.Heart => "Heart",
            MastermindShape.Square => "Square",
            MastermindShape.Circle => "Circle",
            MastermindShape.Triangle => "Triangle",
            _ => null,
        };

        #endregion

        #region Player Input

        /// <summary>
        /// Selects a shape for the player input row. If the row is filled, it will not select a shape.
        /// </summary>
        /// <param name="shape">Name of the shape to select</param>
        /// <exception cref="ArgumentException">Throws when the shape name is not found in the shape enum</exception>
        public void ShapeSelect(string shape)
        {
            if (!Enum.TryParse(shape, out MastermindShape mastermindColor))
            {
                string message = $"Failed to parse shape {shape} as it doesn't exist in the MastermindShape enum.";

                Debug.LogError(message);
                throw new ArgumentException(message);
            }

            ShapeSelect(mastermindColor);
        }

        /// <summary>
        /// Selects a shape for the player input row. If the row is filled, it will not select a shape.
        /// </summary>
        /// <param name="shape">Enum of the shape to select</param>
        private void ShapeSelect(MastermindShape shape)
        {
            if (!HiddenCodeRow.activeInHierarchy) return;
            if (_currentCol == Cols + 1) return;

            _boardRows[_currentRow].transform.Find("c" + _currentCol).GetComponent<Image>().sprite =
                GetSpriteFromShape(shape);
            _playerInputCode.SetValue(MastermindColorToString(shape), _currentCol - 1);
            _currentCol++;
        }

        /// <summary>
        /// Clears the player input row, and sets the current column to 1. 
        /// </summary>
        public void ClearPlayerInputRow()
        {
            for (int i = 1; i < Cols + 1; i++)
            {
                _boardRows[_currentRow].transform.Find("c" + i).GetComponent<Image>().sprite = Empty;
            }

            _playerInputCode = new string[4];
            _currentCol = 1;
        }

        /// <summary>
        /// Replays the minigame by reloading the scene.
        /// THIS DOES NOT WORK AS OF NOW.
        /// TODO: Fix replaying the minigame
        /// </summary>
        [Obsolete("Currently not implemented, likely won't in the future")]
        public void ReplayMinigame()
        {
            SceneLoader.LoadMinigame(MinigameName.Mastermind);
        }

        /// <summary>
        /// Checks the player's input row. If the row is filled, it will check the row for correctness.
        /// After checking the row, it will update the verify icons to show the player how many correct colors and
        /// positions they have. 
        /// </summary>
        public void CheckPlayerInputRow()
        {
            GameObject[] rowItemsToVerify = new GameObject[Cols];
            GameObject[] rowVerifyIcons = new GameObject[Cols];
            GameObject rowToVerify = _boardRows[_currentRow];
            GameObject previousRow = _boardRows[Math.Max(_currentRow - 1, 0)];

            // Get the circles to verify. These are the circles that are in the row and don't have the "Verif" tag
            // These are the circles that will be used to verify the player's input (black and white 2x2 circles)
            for (int i = 0; i < rowItemsToVerify.Length; i++)
            {
                rowItemsToVerify[i] = rowToVerify.transform.Find("c" + (i + 1)).gameObject;
            }

            // Get the verify icons. These are the circles that are in the row and have the "Verif" tag
            // These are the circles that will be used to show the player how many correct colors and positions they have
            // Brackets are used to scope index variable to the loop
            for (int i = 0; i < rowVerifyIcons.Length; i++)
            {
                rowVerifyIcons[i] = rowToVerify.transform.Find("v" + (i + 1)).gameObject;
            }

            // check if the row is filled. If any of the circles are empty, return
            if (rowItemsToVerify.Any(rowItem => rowItem.GetComponent<Image>().sprite == Empty)) return;

            // Set the verify icons to the correct amount of green and yellow circles. Heart = correct position,
            // yellow = correct shape. Fill the rest with red circles
            int goodPositions = GetAmountOfCorrectPositions(_playerInputCode, _secretCodeToGuess);
            int goodColors = GetAmountOfCorrectColors(_playerInputCode, _secretCodeToGuess);

            for (int i = 0; i < goodPositions; i++)
            {
                rowVerifyIcons[i].GetComponent<Image>().sprite = CorrectPosition;
            }

            for (int i = goodPositions; i < goodColors + goodPositions; i++)
            {
                rowVerifyIcons[i].GetComponent<Image>().sprite = CorrectColor;
            }

            for (int i = goodColors + goodPositions; i < Cols; i++)
            {
                rowVerifyIcons[i].GetComponent<Image>().sprite = Incorrect;
            }

            // Check if the player has won or lost
            if (goodPositions == 4)
            {
                Win();
                return;
            }

            if (_currentRow == 9)
            {
                Lose();
                return;
            }

            _currentRow++;

            // TODO: fix color for the current row. Not working as intended
            Color originMastermindColor = rowToVerify.GetComponent<Image>().color;
            Color selectionMastermindColor = originMastermindColor;
            selectionMastermindColor.a = 0.25f;

            rowToVerify.GetComponent<Image>().color = selectionMastermindColor;
            previousRow.GetComponent<Image>().color = originMastermindColor;
        }

        #endregion

        #region Game Initialization

        /// <summary>
        /// Populates the board with the rows for the player to input their guesses.
        /// </summary>
        private void PopulateBoard()
        {
            _boardRows = new GameObject[MaxRows];

            // generate MaxRows amount of rows from the MastermindRow prefab, and set their position, and mount them to the parent
            for (int i = 0; i < MaxRows; i++)
            {
                GameObject row = Instantiate(RowPrefab, new Vector3(RowX, InitialRowY + i * RowYOffset, RowZ),
                    Quaternion.identity);
                row.transform.SetParent(RowParentPrefab.transform);
                row.transform.localScale = new Vector3(1, 1, 1);
                row.layer = 5;
                _boardRows[i] = row;
            }

            // Reverse the array so the first row is at the bottom
            Array.Reverse(_boardRows);
        }

        /// <summary>
        /// Sets the location file for the minigame, as well as generating a new secret codeToCheck for the player to guess.
        /// Inits some variables for the minigame.
        /// </summary>
        public override void PrepareStep()
        {
            SetLocationFile();
            GetNewSecretCode();

            _currentRow = 0; // 0-MaxRows
            _currentCol = 1; // 1-4

            // Add the shape sprites to the dictionary
            _discoSprite.Add("Circle", Circle);
            _discoSprite.Add("Star", Star);
            _discoSprite.Add("Moon", Moon);
            _discoSprite.Add("Heart", Heart);
            _discoSprite.Add("Square", Square);
            _discoSprite.Add("Triangle", Triangle);

            _playerInputCode = new string[Cols];
        }

        /// <summary>
        /// Starts the minigame step. Populates the board.
        /// </summary>
        public override void StartGameStep()
        {
            PopulateBoard();
        }

        /// <summary>
        /// Splits the dialogues for the minigame.
        /// </summary>
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_1"));
            WonDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "mastermindPuzzle_2"));
        }

        /// <summary>
        /// Gets a new secret codeToCheck for the player to guess. Sets the sprites of the secret codeToCheck.
        /// </summary>
        private void GetNewSecretCode()
        {
            // Generate a new codeToCheck to guess
            for (int i = 0; i < Cols; i++)
            {
                int rnd = Random.Range(0, _discoSprite.Count);
                _secretCodeToGuess.SetValue(_discoSprite.ElementAt(rnd).Key, i);
            }

            // Set the sprites of the secret codeToCheck
            for (int i = 0; i < Cols; i++)
            {
                HiddenCodeRow.transform.Find("c" + (i + 1)).GetComponent<Image>().sprite =
                    _discoSprite[_secretCodeToGuess[i]];
            }
        }

        #endregion

        #region Game Ending

        /// <summary>
        /// Called when the player has won the game. Hides the codeToCheck row, and shows the hint for the next location.
        /// </summary>
        private void Win()
        {
            HiddenCodeRow.SetActive(false);
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            CompleteGameStep();
        }

        /// <summary>
        /// Called when the player has lost the game. Hides the codeToCheck row, and shows a message that the player has lost.
        /// </summary>
        private void Lose()
        {
            HiddenCodeRow.SetActive(false);
            LocationUIHintNextLocation.text = "It looks libe you've run out of tries. Better luck next time!";
            CompleteGameStep();
        }

        /// <summary>
        /// Completes the minigame step. Shows the location file.
        /// </summary>
        public override void CompleteGameStep()
        {
            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }

        #endregion

        #region LocationFile

        /// <summary>
        /// Sows the location file UI.
        /// </summary>
        public override void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }

        /// <summary>
        /// Hides the location file UI.
        /// </summary>
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);

            if (LocationFile.IsCompleted)
            {
                ReturnToMap();
            }
        }

        #endregion

        #region Row Verification

        /// <summary>
        /// Gets the amount of good items in the codeToCheck based on the secretCode
        /// </summary>
        /// <param name="codeToCheck">Array to check for correctness</param>
        /// <param name="secretCode">Correct array</param>
        /// <returns>Number of correct items</returns>
        private static int GetAmountOfCorrectPositions(string[] codeToCheck, string[] secretCode) =>
            secretCode.Where((t, i) => codeToCheck[i] == t).Count();

        /// <summary>
        /// Gets the amount of correct colors in the codeToCheck based on the secretCode.
        /// Takes in account <see cref="GetAmountOfCorrectPositions"/>
        /// </summary>
        /// <param name="codeToCheck">Array to check for correctness</param>
        /// <param name="secretCode">Correct array</param>
        /// <returns>Number of correct items</returns>
        private static int GetAmountOfCorrectColors(string[] codeToCheck, string[] secretCode)
        {
            int goodColors = 0;
            List<string> secretCodeList = secretCode.ToList();
            List<string> codeToCheckList = codeToCheck.ToList();

            for (int i = 0; i < codeToCheck.Length; i++)
            {
                if (!secretCodeList.Contains(codeToCheckList[i])) continue;

                goodColors++;
                secretCodeList.Remove(codeToCheckList[i]);
            }

            return goodColors - GetAmountOfCorrectPositions(codeToCheck, secretCode);
        }

        #endregion
    }
}