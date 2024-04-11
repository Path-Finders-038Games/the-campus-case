using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class NavigationDialogue : MonoBehaviour
    {
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;

        public Sprite BuddyCatSprite;

        // Start is called before the first frame update
        void Start()
        {
            BuddyDialogueObject.SetActive(false);
            SetBuddy();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            int currentStep = DataManager.CurrentStep;
            if (DataManager.Language == LanguageManager.Language.English)
            {
                if (!DialogueManager.Instance.EnglishBuddyDialogue.ContainsKey(currentStep)) return;
                
                foreach (Dialogue dialogue in DialogueManager.Instance.EnglishBuddyDialogue[currentStep]
                             .Where(dialogue => dialogue.IsRead != true))
                {
                    SetBuddyDialogueText(dialogue.Text);
                    break;
                }
            }

            if (DataManager.Language == LanguageManager.Language.Dutch)
            {
                if (!DialogueManager.Instance.DutchBuddyDialogue.ContainsKey(currentStep)) return;
                
                foreach (Dialogue dialogue in DialogueManager.Instance.DutchBuddyDialogue[currentStep]
                             .Where(dialogue => dialogue.IsRead != true))
                {
                    SetBuddyDialogueText(dialogue.Text);
                    break;
                }
            }
        }

        private void SetBuddy()
        {
            string buddyChoice = DataManager.Buddy;

            BuddyImage.GetComponent<Image>().sprite = buddyChoice switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite
            };
        }

        private void SetBuddyDialogueText(string Dialogue)
        {
            BuddyTextBlock.text = Dialogue;
            BuddyDialogueObject.SetActive(true);
        }

        public void OnTextBlockClick(string DialoguePerson)
        {
            int currentStep = DataManager.CurrentStep;
            if (DataManager.Language == LanguageManager.Language.English)
            {
                if (DialoguePerson.Equals("Buddy"))
                {
                    string text = BuddyTextBlock.text;
                    BuddyDialogueObject.SetActive(false);
                    DialogueManager.Instance.EnglishBuddyDialogue[currentStep].Find(x => x.Text.Equals(text)).IsRead =
                        true;
                }
            }

            if (DataManager.Language == LanguageManager.Language.Dutch)
            {
                if (DialoguePerson.Equals("Buddy"))
                {
                    string text = BuddyTextBlock.text;
                    BuddyDialogueObject.SetActive(false);
                    DialogueManager.Instance.DutchBuddyDialogue[currentStep].Find(x => x.Text.Equals(text)).IsRead =
                        true;
                }
            }
        }
    }
}