// letter object class
using UnityEngine;

public class WordLetter
{
    // the letter which is being stored as character
    public char Letter;
    //the gameobject related to it
    public GameObject LetterGameObject;

    public WordLetter(char letter, GameObject gameObject)
    {
        this.Letter = letter;
        this.LetterGameObject = gameObject;
    }
}