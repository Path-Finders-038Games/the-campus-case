using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigames
{
    public class KeyboardManager : MonoBehaviour
    {
        private List<GameObject> childrenListLetters = new List<GameObject>();
        private Hangman Hangman;
        List<WordLetter> WordLetters = new List<WordLetter>();

        public Mesh mesh;
        public Dictionary<string,Material> Mats = new Dictionary<string, Material>();
        public GameObject guessworddisplay;
        public Hangman hangman;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("manager startup");
            GetChildObjects(transform);
            Setup();
            WordSetup();
            
            hangman.Guess('e');
           
        }
        

        private void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
            //Debug.Log(guessword.name);
            //wordcheck();
        }

        public void Setup()
        {
            Debug.Log("setup");
            foreach (GameObject go in childrenListLetters)
            {
                char letter = go.name.Last();
                string key = "Paper " + letter;
                Material mat = new Material(go.GetComponent<MeshRenderer>().material);

                Mats.Add(key, mat);
            }
            
        }

        void GetChildObjects(Transform parent)
        {
            int count = 0;
            foreach (Transform child in parent)
            {
                if (count > 0)
                {
                    Debug.Log(child.name);
                    childrenListLetters.Add(child.gameObject);
                }
                count++;
            }
        }

        public void WordSetup()
        {
            float countoffset = 0.42f;
            Debug.Log("entry");
            Debug.Log(hangman.wordletters.Count);
            Debug.Log(hangman.word);
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                Debug.Log(letter.Letter);
                String input = "Paper " + letter.Letter.ToString().ToUpper();
                Debug.Log("de input is" + input);
                Material mat = Mats[input];
                Vector3 scale = new Vector3(.25f, 2, hangman.wordletters.Count+1);
                guessworddisplay.transform.localScale = scale;
                Debug.Log(letter.Letter);
                float ratio = guessworddisplay.transform.localScale.z / guessworddisplay.transform.localScale.y;
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
                gameObject.transform.localScale = new Vector3(10,40,10);
                gameObject.transform.localPosition += offset;
                //gameObject.GetComponent<MeshRenderer>().enabled = false;

                WordLetters.Add(new WordLetter(letter.Letter, gameObject));
                countoffset = countoffset - .84f / (hangman.wordletters.Count-1) ;
                //gameObject.SetActive(false);
               
            }
            Debug.Log("exit");
        }

        public void wordcheck()
        {
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                if(letter.Guessed == true)
                {
                    foreach(WordLetter wordLetter in WordLetters)
                    {
                        Debug.Log("dit vierkant is " + wordLetter.letter +wordLetter.gameObject.GetComponent<MeshRenderer>().enabled);
                        if(wordLetter.letter == letter.Letter)
                        {
                           wordLetter.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            letter.Guessed = false;
                        }
                    }
                }
            }
        }

        private void RaycastCheck()
        {
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
                        Hangman.Guess(input);
                    }
                }
            }
            wordcheck();
        }
    }
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