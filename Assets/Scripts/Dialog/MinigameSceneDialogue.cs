using Navigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class MinigameSceneDialogue : MonoBehaviour
    {
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;

        private bool _isLastText;
        private bool _isDoneTaking;

        void Start()
        {
            BuddyDialogueObject.SetActive(false);
            SetBuddy();
        }

        void Update()
        {
            if (DataManager.CurrentStep <= 3)
            {
                UpdateDialogue();
            }
        }

        private void UpdateDialogue()
        {
            if (_isDoneTaking) return;
            
            if (!_isLastText)
            {
                SetBuddyDialogueText(DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "arExplanation_0"));
            }
            else
            {
                SetBuddyDialogueText(DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "arExplanation_1"));
                _isDoneTaking = true;
            }
        }

        /// <summary>
        /// Sets the buddy image based on the player preferences.
        /// </summary>
        private void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            BuddyImage.GetComponent<Image>().sprite = buddyChoice switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite,
            };
        }

        /// <summary>
        /// Set the buddy dialogue text and activate the dialogue object.
        /// </summary>
        /// <param name="dialogue">Text to display in the dialog.</param>
        private void SetBuddyDialogueText(string dialogue)
        {
            BuddyTextBlock.text = dialogue;
            BuddyDialogueObject.SetActive(true);
        }

        /// <summary>
        /// Deactivate the dialogue object and set the last text to true.
        /// </summary>
        public void OnTextBlockClick()
        {
            BuddyDialogueObject.SetActive(false);
            _isLastText = true;
        }
    }
}