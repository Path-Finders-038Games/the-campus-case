using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangman : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> strings = GetComponent<List<string>>();
        System.Random random = new System.Random();
        Hangman Hangman = new Hangman();
        Hangman.Setup(strings[random.Next()]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private String word { get; set; }
    private List<Character> wordletters = new List<Character>();

    private List<char> wrongletters = new List<char>();
    private List<char> guessedletters = new List<char>();

    private int fout = 0;

    public void Setup(String Word)
    {
        word = Word;
        foreach (char character in word)
        {
            wordletters.Add(new Character(character));
        }
    }

    public void Guess()
    {
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

    public bool won = false;

    public void checkwon()
    {
        won = true;
        foreach (Character letter in wordletters)
        {
            if (letter.Guessed == false)
            {
                won = false;
            }
        }

        if (won)
        {
            Console.WriteLine("gefeliciteerd");
        }
        else if (fout == 6)
        {
            Console.WriteLine("verloren");
            won = true;
        }
    }
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

public class Character
{
    public char Letter;
    public bool Guessed;

    public Character(char letter)
    {
        this.Letter = letter;
        this.Guessed = false;
    }
}