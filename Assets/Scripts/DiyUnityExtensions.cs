using System.Collections.Generic;
using UnityEngine;

namespace Navigation.FreeRoam
{
    public static class DiyUnityExtensions
    {
        public static IEnumerable<GameObject> GetAllChildren(this GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                yield return child.gameObject;

                foreach (GameObject grandChild in GetAllChildren(child.gameObject))
                {
                    yield return grandChild;
                }
            }
        }
    }
}