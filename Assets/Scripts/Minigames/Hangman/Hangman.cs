using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.SocialPlatforms;


/*
 Todo:
-delete console related code
-add animation code
-integrate with project 
-test correct funtionality in unity
-test scaling of objects
 */

namespace Minigames
{

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
        public GameObject Keyboard { get; set; }

        private List<GameObject> KeyboardLetters;

        //the word that is being guessed
        private String word { get; set; }

        //a list of letters that make up the word
        private List<Character> wordletters = new List<Character>();

        //list of incorrectly guessed letters
        //no use at the moment but might be used later
        private List<char> wrongletters = new List<char>();
        //list of guessed letters
        private List<char> guessedletters = new List<char>();

        //counter for the amount of mistakes made
        private int fout = 0;

        // boolean signalling whether the user has won the game 
        public bool won = false;

        //setup method used for setting up the game at the beginning
        //also include potential code for animations etc
        public void Setup()
        {
            LocalizationSetup();
            //setup of the word generation for the game
            System.Random random = new System.Random();
            word = words[random.Next(0, words.Count-1)];
            foreach (char character in word)
            {
                wordletters.Add(new Character(character));
            }
            
            //KeyboardLetters = 
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
                Debug.Log("Key: " + entry.Key + ", Value: " + entry.Value);
                Debug.Log("werkt niet");
            }
        }
        // method used to make a guess
        // also include potential animation code
        public void Guess()
        {
            //snippet checking if the letter has been guessed before
            // duplicate guess check probably made redundant in the final version
            Console.WriteLine("please make a guess");
            char guess;
            while (true)
            {
                guess = Console.ReadKey().KeyChar;
                if (wrongletters.Contains(guess) || guessedletters.Contains(guess))
                {
                    Console.WriteLine();
                    Console.WriteLine("this letter has already been guessed");
                }
                else
                {
                    guessedletters.Add(guess);
                    break;
                }
            }

            //snippet checking if the word has any letters from the guess
            //if not its counted as a incorrect guess
            //potential animations included here also
            Console.WriteLine();
            int count = 0;
            foreach (Character letter in wordletters)
            {
                if (guess == letter.Letter)
                {
                    letter.Guessed = true;
                    count++;
                }
            }
            if (count == 0)
            {
                wrongletters.Add(guess);
                fout++;
                Console.WriteLine("FOUT!");
            }
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
                Console.WriteLine("gefeliciteerd");
            }
        }


        // method for checking if the user has lost the game
        public void CheckLost()
        {
            if (fout == 6)
            {
                Console.WriteLine("verloren");
                won = true;
            }
        }

        //code for playing the game in a console window
        //redundant in final version
        public void ConsoleShow()
        {
            foreach (Character character in wordletters)
            {
                if (character.Guessed == true)
                {
                    Console.Write(character.Letter);
                }
                else
                {
                    Console.Write("*");
                }
            }
            Console.WriteLine();
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