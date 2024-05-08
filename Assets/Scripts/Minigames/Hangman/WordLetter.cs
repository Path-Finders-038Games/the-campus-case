// letter object class
using UnityEngine;

public class WordLetter
{
    // the letter which is being stored as character
    public char letter;
    //the gameobject related to it
    public GameObject gameObject;

    public WordLetter(char letter, GameObject gameObject)
    {
        this.letter = letter;
        this.gameObject = gameObject;
    }
}