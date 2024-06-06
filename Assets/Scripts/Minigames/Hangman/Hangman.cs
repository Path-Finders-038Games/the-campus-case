using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Dialog;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;


namespace Minigames
{
    // class for all logic related to the hangman minigame
    public class Hangman : Minigame
    {
        //list of guessed letters
        private List<char> _guessedLetters = new();

        //counter for the amount of mistakes made
        private int _strikes = 0;

        private bool _lost = false;

        //list of potential words for the game to select
        public List<string> Words = new();

        //the word that is being guessed
        public string Word { get; set; }

        //a list of letters that make up the word
        public List<Character> WordLetters = new();
   
        // boolean to decide if the game is currently being played
        public bool Playing;

        //variable for Executing animations
        public AnimationManager AnimationManagerProperty;

        public Button HideLocationFileButton;

        /// <summary>
        /// Sanitizes the word by removing spaces and other unwanted characters.
        /// </summary>
        /// <param name="word">Word to sanitize.</param>
        /// <returns>Sanitized word.</returns>
        private static string SanitizeWord(string word)
        {
            // Remove spaces
            word = word.Replace(" ", "");

            // Replace characters like ', _ and - with nothing
            word = Regex.Replace(word, "[\'_-]", "");

            // Replace characters like é with e
            word = RemoveDiacritics(word);

            return word;
        }
        //method for returning special characters to their normal state
        static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        // method used to make a guess
        public void Guess(char guess)
        {
            char guessLower = char.ToLower(guess);
            //snippet checking if the letter has been guessed before: not needed letters dissapear after guessing them in keyboardmanager
            // duplicate guess check probably made redundant in the final version
            _guessedLetters.Add(guess);

            //snippet checking if the word has any letters from the guess
            //if not its counted as a incorrect guess
            //and animation is played
            int count = 0;
            foreach (Character letter in WordLetters.Where(letter => guessLower == letter.Letter))
            {
                letter.Guessed = true;
                count++;
            }

            if (count == 0)
            {
                _strikes++;
                AnimationManagerProperty.AssemblePart();
            }

            CheckLost();
            CheckWon();
        }

        // method checking if the user has completely guessed the word
        public void CheckWon()
        {
            //checks if the user has won the game
            bool won = true;

            foreach (Character letter in WordLetters.Where(letter => letter.Guessed == false))
            {
                won = false;
            }

            //code for what to do when the user has won
            if (won)
            {
                CompleteGameStep();
            }
        }

        // method for checking if the user has lost the game
        public void CheckLost()
        {
            if (_strikes >= 12)
            {
                ReturnGameStep();
            }
        }

        //setup method used for setting up the game at the beginning
        public override void PrepareStep()
        {
            List<string> localizedWords = DialogueManagerV2.GetAllLocalizedStrings("Minigame 6-localization");

            // Check if words have spaces, if so, remove the spaces and log an error
            foreach (string sanitizedWord in localizedWords.Select(SanitizeWord))
            {
                Words.Add(sanitizedWord);
            }

            //setup of the word generation for the game
            System.Random random = new System.Random();
            Word = Words[random.Next(0, Words.Count)];
            foreach (char character in Word)
            {
                WordLetters.Add(new Character(char.ToLower(character)));
            }

            Playing = false;
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
        }

        public override void SplitDialogue()
        {
            //niet in gebruik atm
        }

        public override void StartGameStep()
        {
            ShowLocationFile();
        }

        /// <summary>
        /// Completes the minigame step. Shows the location file with updated text.
        /// </summary>
        public override void CompleteGameStep()
        {
            //receives the text in a single string which gets split up
            string CompleteText = DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "Hangman_win");
            string[] separatedStrings = CompleteText.Split('-');
            Playing = false;
            LocationUIName.text = separatedStrings[0];
            LocationUIFacts.text = "";
            LocationUIDescription.text = separatedStrings[1];
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }

        /// <summary>
        /// Method for when the player loses the game and is returned to the map.
        /// </summary>
        public void ReturnGameStep()
        {
            //receives the text in a single string which gets split up
            string CompleteText = DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "Hangman_loss");
            string[] separatedStrings = CompleteText.Split('-');
            Playing = false;
            _lost = true;
            LocationUIName.text = separatedStrings[0]; ;
            LocationUIFacts.text = "";
            LocationUIDescription.text = LocationUIDescription.text = separatedStrings[1]; ;
            ShowLocationFile();
        }

        /// <summary>
        /// Sows the location file UI.
        /// </summary>
        public override void ShowLocationFile()
        {
            Playing = false;
            LocationFileUI.SetActive(true);
        }

        /// <summary>
        /// Hides the location file UI.
        /// </summary>
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            Playing = true;

            if (LocationFile.IsCompleted || _lost)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}