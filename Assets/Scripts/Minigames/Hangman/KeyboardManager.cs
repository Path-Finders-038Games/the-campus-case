using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 add dialogue to the game
optimize wordcheck
 */

namespace Minigames
{
    // class responsible for the interaction between the user and the hangman game
    public class KeyboardManager : MonoBehaviour
    {
        // list of all letters on the guessingboard
        private List<GameObject> _childrenListLetters = new List<GameObject>();

        //list of all letters in the word
        private List<WordLetter> _wordLetters = new List<WordLetter>();

        // mesh of the letters
        public Mesh LetterMesh;

        //dictionary of all materials used by the letters
        public Dictionary<string,Material> Mats = new Dictionary<string, Material>();

        //object as background for the guessword to be displayed against
        public GameObject GuesswordDisplay;

        //hangman game
        public Hangman HangmanGame;
        // Start is called before the first frame update
        void Start()
        {
            GetChildObjects(transform);
            Setup();
            WordSetup();
        }
        

        private void Update()
        {
            if (!HangmanGame.Playing) return;

            //checks if the user is touching the screen and fires a raycast
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
        }

        //gets the material of each letter and makes a name for it and puts it into the dictionary
        public void Setup()
        {
            
            foreach (GameObject go in _childrenListLetters)
            {
                char letter = go.name.Last();
                string key = "Paper " + letter;
                Material mat = new Material(go.GetComponent<MeshRenderer>().material);

                Mats.Add(key, mat);
            }
            
        }

        //gets all child objects of the clipboard by their transform component
        //uses count to step over the clipboard object
        void GetChildObjects(Transform parent)
        {
            int count = 0;
            foreach (Transform child in parent)
            {
                if (count > 0)
                {
                    _childrenListLetters.Add(child.gameObject);
                }
                count++;
            }
        }

        //setup method for the guessworddisplay
        public void WordSetup()
        {
            //offset in front of the backboard 
            float countoffset = 0.42f;
            //size of the display
            Vector3 scale = new Vector3(.25f, 2, HangmanGame.WordLetters.Count + 2);
            GuesswordDisplay.transform.localScale = scale;

            foreach (Character letter in HangmanGame.WordLetters)
            {
                //get the correct letter graphics
                String input = "Paper " + letter.Letter.ToString().ToUpper();
                Material mat = Mats[input];
                
                //assign offset value to place letter in front of the board
                Vector3 offset =new Vector3(-.55f, 0, 0);
                offset.z = countoffset;

                //create a new gameobject for the letter
                GameObject gameObject = new GameObject();
                gameObject.name = letter.Letter.ToString();
                gameObject.AddComponent<MeshFilter>().mesh = LetterMesh;
                gameObject.AddComponent<MeshRenderer>().material = mat;

                //correctly place the letter
                gameObject.transform.position = GuesswordDisplay.transform.position;
                gameObject.transform.rotation = GuesswordDisplay.transform.rotation;
                gameObject.transform.Rotate(0,-90,0);
                gameObject.transform.localScale = GuesswordDisplay.transform.localScale;
                gameObject.transform.SetParent(GuesswordDisplay.transform);
                gameObject.transform.localScale = new Vector3((50/HangmanGame.WordLetters.Count), 24, (1/GuesswordDisplay.transform.localScale.x));
                gameObject.transform.localPosition += offset;
                gameObject.GetComponent<MeshRenderer>().enabled = false;

                _wordLetters.Add(new WordLetter(letter.Letter, gameObject));
                countoffset = countoffset - .84f / (HangmanGame.WordLetters.Count-1) ;
               
            }
        }

        //check to see if letters should be turned visible
        public void Wordcheck()
        {
            foreach (Character letter in HangmanGame.WordLetters)
            {
                if(letter.Guessed == true)
                {
                    foreach(WordLetter wordLetter in _wordLetters)
                    {
                       
                        if(wordLetter.letter == letter.Letter)
                        {
                           wordLetter.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
        }
        //checks where the user touches the AR object and guesses the selected letter
        private void RaycastCheck()
        {
            RaycastHit hit;
            Vector2 touchpos = Input.touches[0].position;
            Ray ray = Camera.main.ScreenPointToRay(touchpos);
            if (Physics.Raycast(ray, out hit, 100))
           {
                string name = hit.transform.gameObject.name;
                foreach (GameObject child in _childrenListLetters)
                {

                    if (child.name == name)
                    {
                        char input = name.Last();
                        HangmanGame.Guess(input);
                        child.SetActive(false);
                    }
                }
                Wordcheck();
            }
           
           
        }
    }
}