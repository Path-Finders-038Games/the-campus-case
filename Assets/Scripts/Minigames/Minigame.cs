using Dialog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{
    public abstract class Minigame : MonoBehaviour
    {
        public LocationFile LocationFile;
        //Getting everything the buddy needs to say and do
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;
        
        //getting the end and start dialogue
        public List<Dialogue> StartMinigame = new();
        public List<Dialogue> EndMinigame = new();
        
        //Getting the location info
        public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;


        public void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            BuddyImage.GetComponent<Image>().sprite = buddyChoice switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite,
            };
        }
        public abstract void SplitDialogue();
        public abstract void PrepareStep();
        public abstract void StartGameStep();
        public abstract void CompleteGameStep();
        public abstract void ShowLocationFile();
        public abstract void HideLocationFile();
        public void SetLocationFile()
        {
            LocationFile = LanguageManager.GetLanguage() switch
            {
                LanguageManager.Language.Dutch => DutchFile(),
                LanguageManager.Language.English => EnglishFile(),
                _ => LocationFile
            };
            
            LocationUIName.text = LocationFile.Name;
            LocationUIDescription.text = LocationFile.Description;

            StringBuilder locationFacts = new("Facts\n");
            foreach (string fact in LocationFile.Facts)
            {
                locationFacts.AppendLine(fact + "\n");
            }

            LocationUIFacts.text = locationFacts.ToString();
        }
        public LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description,
                LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);
        }

        public LocationFile EnglishFile()
        {
            return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description,
                LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);
        }
        public void SetBuddyDialogueText(string Dialogue)
        {
            BuddyTextBlock.text = Dialogue;
            BuddyDialogueObject.SetActive(true);
        }
        public void OnTextBlockClick()
        {
            string text = BuddyTextBlock.text;

            if (!LocationFile.IsCompleted)
            {
                StartMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }
            else
            {
                EndMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }

            BuddyDialogueObject.SetActive(false);
        }
        public void UpdateDialogue()
         {
            if (!LocationFile.IsCompleted)
            {
                Dialogue dialogue = StartMinigame.First(dialogue => dialogue.IsRead != true);
                SetBuddyDialogueText(dialogue.Text);
            }
            else
            {
                Dialogue dialogue = EndMinigame.First(dialogue => dialogue.IsRead != true);
                SetBuddyDialogueText(dialogue.Text);
            }
        }
    }
}