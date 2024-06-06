using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class MinigameTileStatus : MonoBehaviour
    {
        public MinigameName Minigame;
        public Image ButtonPicture;
        public Sprite CompletedSprite;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.interactable = true;
        }

        private void Update()
        {
            if (!_button.interactable) return;
            if (!DataManager.MinigameStatus[Minigame]) return;
            
            _button.interactable = false;
            ButtonPicture.sprite = CompletedSprite;
        }
    }
}