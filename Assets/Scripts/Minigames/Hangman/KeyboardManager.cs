using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigames
{
    // class responsible for the interaction between the user and the hangman game
    public class KeyboardManager : MonoBehaviour
    {
        // list of all letters on the guessingboard
        private List<GameObject> childrenListLetters = new List<GameObject>();

        //list of all letters in the word
        List<WordLetter> WordLetters = new List<WordLetter>();

        // mesh of the letters
        public Mesh mesh;

        //dictionary of all materials used by the letters
        public Dictionary<string,Material> Mats = new Dictionary<string, Material>();

        //object as background for the guessword to be displayed against
        public GameObject guessworddisplay;

        //hangman game
        public Hangman hangman;
        // Start is called before the first frame update
        void Start()
        {
            GetChildObjects(transform);
            Setup();
            WordSetup();
        }
        

        private void Update()
        {
            //checks if the user is touching the screen and fires a raycast
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
            //Debug.Log(guessword.name);
            //wordcheck();
        }

        //gets the material of each letter and makes a name for it and puts it into the dictionary
        public void Setup()
        {
            
            foreach (GameObject go in childrenListLetters)
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
                    childrenListLetters.Add(child.gameObject);
                }
                count++;
            }
        }

        //setup method for the guessworddisplay
        public void WordSetup()
        {
            float countoffset = 0.42f;
            Vector3 scale = new Vector3(.25f, 2, hangman.wordletters.Count + 2);
            guessworddisplay.transform.localScale = scale;
            int amount = hangman.wordletters.Count;
            Vector3 space =  guessworddisplay.transform.localScale;
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                String input = "Paper " + letter.Letter.ToString().ToUpper();
                Material mat = Mats[input];
                
                Vector3 offset =new Vector3(-.55f, 0, 0);
                offset.z = countoffset;
                GameObject gameObject = new GameObject();
                gameObject.name = letter.Letter.ToString();
                gameObject.AddComponent<MeshFilter>().mesh = mesh;
                gameObject.AddComponent<MeshRenderer>().material = mat;

                gameObject.transform.position = guessworddisplay.transform.position;
                gameObject.transform.rotation = guessworddisplay.transform.rotation;
                gameObject.transform.Rotate(0,-90,0);
                gameObject.transform.localScale = guessworddisplay.transform.localScale;
                gameObject.transform.SetParent(guessworddisplay.transform);
                gameObject.transform.localScale = new Vector3((50/hangman.wordletters.Count), 24, (1/guessworddisplay.transform.localScale.x));
                gameObject.transform.localPosition += offset;
                gameObject.GetComponent<MeshRenderer>().enabled = false;

                WordLetters.Add(new WordLetter(letter.Letter, gameObject));
                countoffset = countoffset - .84f / (hangman.wordletters.Count-1) ;
               
            }
        }

        //check to see if letters should be turned visible
        public void Wordcheck()
        {
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                Debug.Log(letter.Letter + " = " + letter.Guessed);
                if(letter.Guessed == true)
                {
                    foreach(WordLetter wordLetter in WordLetters)
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
            Debug.Log(hangman.word);
            RaycastHit hit;
            Vector2 touchpos = Input.touches[0].position;
            Ray ray = Camera.main.ScreenPointToRay(touchpos);
            if (Physics.Raycast(ray, out hit, 100))
           {
                string name = hit.transform.gameObject.name;
                foreach (GameObject child in childrenListLetters)
                {

                    if (child.name == name)
                    {
                        char input = name.Last();
                        hangman.Guess(input);
                    }
                }
                Wordcheck();
            }
           
           
        }
    }
    // letter object class
    public class WordLetter
    {
        public char letter;
        public GameObject gameObject;

        public WordLetter(char letter,GameObject gameObject)
        {
            this.letter = letter;
            this.gameObject = gameObject;
        }
    }
}