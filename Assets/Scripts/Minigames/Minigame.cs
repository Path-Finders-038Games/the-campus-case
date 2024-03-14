using System;
using Dialog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{
    public enum GameState
    {
        Tutorial,
        Playing,
        Won,
        Lost,
    }
    
    public abstract class Minigame : MonoBehaviour
    {
        protected LocationFile LocationFile;

        // Buddy dialogue.
        // These will be set in the Unity editor.
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;

        // Dialogue lists.
        // These will be set in the minigame itself.
        protected List<Dialogue> TutorialDialogues = new();
        protected List<Dialogue> WonDialogues = new();
        protected List<Dialogue> LostDialogues = new();

        // Location info.
        // These will be set in the Unity editor.
        public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;

        // Scenes.
        // These will be set in the Unity editor.
        public GameObject TutorialScene;
        public GameObject PlayingScene;
        public GameObject WonScene;
        public GameObject LostScene;
        
        // Game state. This will keep track of the current game state, execute logic and switch scenes when set.
        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                ProcessGameState(_gameState);
            }
        }
        
        public abstract void SplitDialogue();
        public abstract void PrepareStep();
        public abstract void StartGameStep();
        public abstract void CompleteGameStep();
        public abstract void ShowLocationFile();
        public abstract void HideLocationFile();
        
        /// <summary>
        /// Processes the game state and switches the scene accordingly.
        /// Gets called when the game state is set.
        /// </summary>
        /// <param name="gameState">Current game state</param>
        private void ProcessGameState(GameState gameState)
        {
            // TODO: Add logic for each game state?
            
            // switch the scene based on the game state
            SwitchScene(gameState);
        }
        
        /// <summary>
        /// Switches the scene based on the game state.
        /// </summary>
        /// <param name="gameState">State to switch to.</param>
        private void SwitchScene(GameState gameState)
        { 
            // hide all scenes and show the one that is needed
            TutorialScene.SetActive(gameState == GameState.Tutorial);
            PlayingScene.SetActive(gameState == GameState.Playing);
            WonScene.SetActive(gameState == GameState.Won);
            LostScene.SetActive(gameState == GameState.Lost);
        }

        protected void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            BuddyImage.GetComponent<Image>().sprite = buddyChoice switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite,
            };
        }

        protected void SetLocationFile()
        {
            LocationFile = LanguageManager.GetLanguage() switch
            {
                LanguageManager.Language.Dutch => DutchFile(),
                LanguageManager.Language.English => EnglishFile(),
                _ => LocationFile,
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

        private LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description,
                LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);
        }

        private LocationFile EnglishFile()
        {
            return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description,
                LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);
        }

        private void SetBuddyDialogueText(string dialogue)
        {
            BuddyTextBlock.text = dialogue;
            BuddyDialogueObject.SetActive(true);
        }

        public void OnTextBlockClick()
        {
            string text = BuddyTextBlock.text;

            if (LocationFile.IsCompleted)
            {
                if (GameState == GameState.Won)
                {
                    WonDialogues.Find(x => x.Text.Equals(text)).IsRead = true;
                }
                else
                {
                    LostDialogues.Find(x => x.Text.Equals(text)).IsRead = true;
                }
            }
            else
            {
                TutorialDialogues.Find(x => x.Text.Equals(text)).IsRead = true;
            }

            BuddyDialogueObject.SetActive(false);
        }

        protected void UpdateDialogue()
        {
            Dialogue dialogue = LocationFile.IsCompleted
                ? GameState == GameState.Won
                    ? WonDialogues.First(dialogue => dialogue.IsRead != true)
                    : LostDialogues.First(dialogue => dialogue.IsRead != true)
                : TutorialDialogues.First(dialogue => dialogue.IsRead != true);
            SetBuddyDialogueText(dialogue.Text);
        }
    }
}