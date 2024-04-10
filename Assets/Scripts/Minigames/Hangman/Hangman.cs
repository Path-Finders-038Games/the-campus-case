using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Dialog;
using Navigation;


/*
 Todo:
-improve presentation to user(text on win or lose)
-update to new minigamea standards
 */

namespace Minigames
{
    // class for all logic related to the hangman minigame
    public class Hangman : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            // create the instance of the game and run the setup method to make it ready for playing
            Hangman Hangman = new();
            Setup();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        //list of potential words for the game to select
        public List<string> words = new();

        //the word that is being guessed
        public string word { get; set; }

        //a list of letters that make up the word
        public List<Character> wordletters = new();

        //list of guessed letters
        private List<char> guessedletters = new();

        //counter for the amount of mistakes made
        private int fout = 0;

        public AnimationManager animationManager;

        //setup method used for setting up the game at the beginning
        public void Setup()
        {
            List<string> localizedWords = DialogueManagerV2.GetAllLocalizedStrings("Minigame 6 localization");

            // Check if words have spaces, if so, remove the spaces and log an error
            foreach (string sanitizedWord in localizedWords.Select(SanitizeWord))
            {
                words.Add(sanitizedWord);
            }

            //setup of the word generation for the game
            System.Random random = new System.Random();
            word = words[random.Next(0, words.Count)];
            foreach (char character in word)
            {
                wordletters.Add(new Character(character));
            }
        }

        /// <summary>
        /// Sanitizes the word by removing spaces and other unwanted characters.
        /// </summary>
        /// <param name="word">Word to sanitize.</param>
        /// <returns>Sanitized word.</returns>
        // TODO: Move this method to a separate class
        private static string SanitizeWord(string word)
        {
            word = word.Replace(" ", "");

            // TODO: Replace characters like ', _ and - with nothing
            // TODO: Replace characters like é with e, etc.

            return word;
        }

        // method used to make a guess
        public void Guess(char guess)
        {
            char guessLower = char.ToLower(guess);
            //snippet checking if the letter has been guessed before
            // duplicate guess check probably made redundant in the final version
            guessedletters.Add(guess);

            //snippet checking if the word has any letters from the guess
            //if not its counted as a incorrect guess
            //potential animations included here also
            int count = 0;
            foreach (Character letter in wordletters.Where(letter => guessLower == letter.Letter))
            {
                letter.Guessed = true;
                count++;
            }

            if (count == 0)
            {
                fout++;
                animationManager.AssemblePart();
            }

            CheckLost();
            CheckWon();
        }

        // method checking if the user has completely guessed the word
        public void CheckWon()
        {
            //checks if the user has won the game
            bool won = true;

            foreach (Character letter in wordletters.Where(letter => letter.Guessed == false))
            {
                won = false;
            }

            //code for what to do when the user has won
            if (won)
            {
                //needs work
                
                DataManager.SetMinigameStatus(MinigameName.Hangman, true);
                SceneLoader.LoadScene(GameScene.Navigation);
            }
        }

        // method for checking if the user has lost the game
        public void CheckLost()
        {
            if (fout >= 12)
            {
                //still needs work
                SceneLoader.LoadScene(GameScene.Navigation);
            }
        }

        // class for each individual letter in the word 
        public class Character
        {
            // letter itself
            public char Letter;

            // if it has been guessed yet or not
            public bool Guessed;

            public Character(char letter)
            {
                this.Letter = letter;
                this.Guessed = false;
            }
        }
    }
}