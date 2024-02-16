using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 Todo:
-delete console related code
-add animation code
-integrate with project 
-test correct funtionality in unity
 */

public class Hangman : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //setup of the word generation for the game
        // needs to retrieve a list of words depending on language selected
        List<string> strings = GetComponent<List<string>>();
        System.Random random = new System.Random();

        // create the instance of the game and run the setup method to make it ready for playing
        Hangman Hangman = new Hangman();
        Hangman.Setup(strings[random.Next()]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
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
    public void Setup(String Word)
    {
        word = Word;
        foreach (char character in word)
        {
            wordletters.Add(new Character(character));
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