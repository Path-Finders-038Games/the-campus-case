using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class MinigameTileStatus : MonoBehaviour
    {
        public MinigameName Minigame;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.interactable = true;
        }

        private void Update()
        {
            if (DataManager.MinigameStatus[Minigame])
            {
                _button.interactable = false;
            }
        }
    }
}