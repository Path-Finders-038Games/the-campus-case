using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using System.Collections;


/*
 Todo:
-add animation code
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
            Hangman Hangman = new Hangman();
            Setup();

        }

        // Update is called once per frame
        private void Update()
        {
        }




        //list of potential words for the game to select
        public List<String> words = new List<String>();

        //the word that is being guessed
        public String word { get; set; }

        //a list of letters that make up the word
        public List<Character> wordletters = new List<Character>();

        //list of incorrectly guessed letters
        //no use at the moment but might be used later
        //private List<char> wrongletters = new List<char>();

        //list of guessed letters
        private List<char> guessedletters = new List<char>();

        //counter for the amount of mistakes made
        private int fout = 0;

        // boolean signalling whether the user has won the game 
        public bool won = false;

        //setup method used for setting up the game at the beginning
        public void Setup()
        {
            LocalizationSetup();
            //setup of the word generation for the game
            System.Random random = new System.Random();
            word = words[random.Next(0, words.Count)];
            foreach (char character in word)
            {
                wordletters.Add(new Character(character));
            }

        }

        //retrieve list of words for the game to choose between based on selected language
        public void LocalizationSetup()
        {
            // set locale
            // ...
            // Access the localization table collection
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;

            // Get the String Table from the collection
            StringTable stringTable = tableCollection.GetTable("Minigame 6 localization");

            // Get all entries in the table
            ICollection<StringTableEntry> entries = stringTable.Values;

            // Iterate through each entry and print key-value pairs
            foreach (var entry in entries)
            {
                words.Add(entry.Value);
            }
        }
        // method used to make a guess
        public void Guess(char guess)
        {
            char guessUpper = char.ToUpper(guess);
            char guessLower = char.ToLower(guess);
            //snippet checking if the letter has been guessed before
            // duplicate guess check probably made redundant in the final version
            guessedletters.Add(guess);     

            //snippet checking if the word has any letters from the guess
            //if not its counted as a incorrect guess
            //potential animations included here also
            int count = 0;
            foreach (Character letter in wordletters)
            {
                if (guessUpper == letter.Letter || guessLower == letter.Letter)
                {
                    letter.Guessed = true;
                    count++;
                }
            }
            if (count == 0)
            {
                //wrongletters.Add(guess);
                fout++;
            }

            CheckLost();
            CheckWon();
        }


        // method checking if the user has completely guessed the word
        public void CheckWon()
        {

            //checks if the user has won the game
            won = true;
            foreach (Character letter in wordletters)
            {
                if (letter.Guessed == false)
                {
                    won = false;
                }
            }

            //code for what to do when the user has won
            if (won)
            {
                //needs work
                SceneManager.LoadScene(1);
            }
        }


        // method for checking if the user has lost the game
        public void CheckLost()
        {
            if (fout == 6)
            {
                //still needs work
                SceneManager.LoadScene(1);
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