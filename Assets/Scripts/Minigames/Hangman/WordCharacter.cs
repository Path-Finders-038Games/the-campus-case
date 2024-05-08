// class for each individual letter in the word 
using UnityEngine;

public class Character : MonoBehaviour
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