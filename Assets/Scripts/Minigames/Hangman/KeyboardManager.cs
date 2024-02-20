using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{
    public class KeyboardManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public List<GameObject> childrenList;

        void Start()
        {
            childrenList = new List<GameObject>();
            GetChildObjects(transform);
        }

        void GetChildObjects(Transform parent)
        {
            foreach (Transform child in parent)
            {
                childrenList.Add(child.gameObject);
                GetChildObjects(child);
            }
        }
    }
}