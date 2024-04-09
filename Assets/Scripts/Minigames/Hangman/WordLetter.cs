// letter object class
using UnityEngine;

public class WordLetter
{
    public char letter;
    public GameObject gameObject;

    public WordLetter(char letter, GameObject gameObject)
    {
        this.letter = letter;
        this.gameObject = gameObject;
    }
}