using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{

    public class InputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetChildRecursive(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public List<GameObject> listOfLetters;
        private void GetChildRecursive(GameObject obj)
        {
            if (null == obj)
                return;

            foreach (Transform child in obj.transform)
            {
                if (null == child)
                    continue;
                //child.gameobject contains the current child you can do whatever you want like add it to an array
                listOfLetters.Add(child.gameObject);
            }
        }
    }
}