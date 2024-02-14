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
        private bool _isLastText = false;
        private bool _isDoneTaking = false;
        // Start is called before the first frame update
        void Start()
        {
            BuddyDialogueObject.SetActive(false);
            SetBuddy();
        }

        // Update is called once per frame
        void Update()
        {
            if (DataManager.Instance.CurrentStep <= 3)
            {
                UpdateDialogue();
            }  
        }
        private void UpdateDialogue()
        {
            if (!_isDoneTaking)
            {
                if (PlayerPrefs.GetString("Language").Equals("EN"))
                {
                    if (!_isLastText)
                    {
                        SetBuddyDialogueText(DialogueManager.Instance.EnglishBuddyDialogue[-1][0].Text);
                    }
                    else
                    {
                        SetBuddyDialogueText(DialogueManager.Instance.EnglishBuddyDialogue[-1][1].Text);
                        _isDoneTaking = true;
                    }
                }
                if (PlayerPrefs.GetString("Language").Equals("NL"))
                {
                    if (!_isLastText)
                    {
                        SetBuddyDialogueText(DialogueManager.Instance.DutchBuddyDialogue[-1][0].Text);
                    }
                    else
                    {
                        SetBuddyDialogueText(DialogueManager.Instance.DutchBuddyDialogue[-1][1].Text);
                        _isDoneTaking = true;
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
        public void OnTextBlockClick()
        {
            BuddyDialogueObject.SetActive(false);
            _isLastText = true;
        }
    }
}
