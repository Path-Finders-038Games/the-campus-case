using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Minigames
{
    public class KeyboardManager : MonoBehaviour
    {
        private List<GameObject> childrenListLetters = new List<GameObject>();
        private Hangman Hangman;

        List<GameObject> WordLetters = new List<GameObject>();
        public Mesh Mesh;
        public GameObject guessword;
        public Hangman hangman;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("manager startup");
            GetChildObjects(transform);
            WordSetup();
        }

        private void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
            //Debug.Log(guessword.name);
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
            int count = 1;
            Debug.Log("entry");
            Debug.Log(hangman.wordletters.Count);
            
            foreach (Hangman.Character letter in hangman.wordletters)
            {
                Debug.Log(letter.Letter);
                Vector3 offset =new Vector3(count, 0, 0);
                GameObject gameObject = new GameObject();
                gameObject.name = letter.Letter.ToString();
                gameObject.AddComponent<MeshFilter>().mesh = Mesh;
                gameObject.AddComponent<MeshRenderer>();
                gameObject.transform.SetParent(guessword.transform);
                gameObject.transform.position += offset;
                WordLetters.Add(gameObject);
                gameObject.gameObject.SetActive(true);
                count++;
            }
            Debug.Log("exit");
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
        }
    }
}