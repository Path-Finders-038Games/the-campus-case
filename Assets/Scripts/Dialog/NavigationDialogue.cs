using Navigation;
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
            int CurrentStep = DataManager.Instance.CurrentStep;
            if (PlayerPrefs.GetString("Language").Equals("EN"))
            {
                if (DialogueManager.Instance.EnglishBuddyDialogue.ContainsKey(CurrentStep))
                {
                    foreach (Dialogue dialogue in DialogueManager.Instance.EnglishBuddyDialogue[CurrentStep])
                    {
                        if (dialogue.IsRead != true)
                        {
                            SetBuddyDialogueText(dialogue.Text);
                            break;
                        }
                    }
                }
            }
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {

                if (DialogueManager.Instance.DutchBuddyDialogue.ContainsKey(CurrentStep))
                {
                    foreach (Dialogue dialogue in DialogueManager.Instance.DutchBuddyDialogue[CurrentStep])
                    {
                        if (dialogue.IsRead != true)
                        {
                            SetBuddyDialogueText(dialogue.Text);
                            break;
                        }
                    }
                }
            }
        }
        private void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            if (buddyChoice.Equals("Cat"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyCatSprite;
            }
            if (buddyChoice.Equals("Dog"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyDogSprite;
            }
        }
        private void SetBuddyDialogueText(string Dialogue)
        {
            BuddyTextBlock.text = Dialogue;
            BuddyDialogueObject.SetActive(true);
        }
        public void OnTextBlockClick(string DialoguePerson)
        {
            int CurrentStep = DataManager.Instance.CurrentStep;
            if (PlayerPrefs.GetString("Language").Equals("EN"))
            {
                if (DialoguePerson.Equals("Buddy"))
                {
                    string text = BuddyTextBlock.text;
                    BuddyDialogueObject.SetActive(false);
                    DialogueManager.Instance.EnglishBuddyDialogue[CurrentStep].Find(x => x.Text.Equals(text)).IsRead = true;
                
                }
            }
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                if (DialoguePerson.Equals("Buddy"))
                {
                    string text = BuddyTextBlock.text;
                    BuddyDialogueObject.SetActive(false);
                    DialogueManager.Instance.DutchBuddyDialogue[CurrentStep].Find(x => x.Text.Equals(text)).IsRead = true;

                }
            }
        }
    }
}
