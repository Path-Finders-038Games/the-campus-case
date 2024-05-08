using UnityEngine;

namespace Minigames.Sliding_puzzel
{
    public class SlidingPuzzleTile : MonoBehaviour
    {
        public int Id;
        public Vector2Int Position;

        public string Name;

        private void Awake()
        {
            Name = gameObject.name;
        }
    }
}