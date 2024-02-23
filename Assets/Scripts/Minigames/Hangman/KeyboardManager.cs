using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minigames
{
    public class KeyboardManager : MonoBehaviour
    {
        private List<GameObject> childrenListLetters = new List<GameObject>();
        private Hangman Hangman;

        List<WordLetter> WordLetters = new List<WordLetter>();
        public Mesh Mesh;
        public Material mat;
        public GameObject guessworddisplay;
        public Hangman hangman;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("manager startup");
            GetChildObjects(transform);
            WordSetup();
            hangman.Guess('e');
            Debug.Log("gegokt");
            Debug.Log("AHHHHHHHHH");
        }
        

        private void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
            //Debug.Log(guessword.name);
            wordcheck();
        }

        void GetChildObjects(Transform parent)
        {
            Debug.Log(parent.name);
            foreach (Transform child in parent)
            {
                Debug.Log(child.name);
                childrenListLetters.Add(child.gameObject);
            }
        }

        public void WordSetup()
        {
            float count = -0.4f;
            Debug.Log("entry");
            Debug.Log(hangman.wordletters.Count);
            
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                Debug.Log(letter.Letter);
                Vector3 offset =new Vector3(.5f, 0, 0);
                offset.z = count;
                GameObject gameObject = new GameObject();
                gameObject.name = letter.Letter.ToString();
                gameObject.AddComponent<MeshFilter>().mesh = Mesh;
                gameObject.AddComponent<MeshRenderer>().material = mat;

                gameObject.transform.position = guessworddisplay.transform.position;
                gameObject.transform.rotation = guessworddisplay.transform.rotation;
                gameObject.transform.localScale = guessworddisplay.transform.localScale;
                gameObject.transform.SetParent(guessworddisplay.transform);
                gameObject.transform.localScale = new Vector3(.25f,.5f, .1f);
                gameObject.transform.localPosition += offset;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                
                WordLetters.Add(new WordLetter(letter.Letter,gameObject));
                //gameObject.SetActive(false);
                count = count + .1F;
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