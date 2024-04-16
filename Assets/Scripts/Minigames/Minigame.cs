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
        // Buddy dialogue.
        // These will be set in the Unity editor.
        [Header("Buddy Dialogue")]
        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;

        // Dialogue lists.
        // These will be set in the minigame itself.
        protected readonly List<Dialogue> TutorialDialogues = new();
        protected readonly List<Dialogue> WonDialogues = new();
        protected readonly List<Dialogue> LostDialogues = new();

        // Location info.
        // These will be set in the Unity editor.
        protected LocationFile LocationFile;
        [Header("Location File")]
        public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;

        // Scenes.
        // These will be set in the Unity editor.
        // public GameObject TutorialScene;
        // public GameObject PlayingScene;
        // public GameObject WonScene;
        // public GameObject LostScene;
        
        // Game state. This will keep track of the current game state, execute logic and switch scenes when set.
        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                // ProcessGameState(_gameState);
            }
        }
        
        /// <summary>
        /// Starts the minigame. Runs before the first frame update.
        /// If you want to add custom logic, override this and call the base method.
        /// </summary>
        /// <example>
        /// <code>
        /// public override void Start()
        /// {
        ///     // custom logic
        ///     base.Start();
        /// }
        /// </code>
        /// </example>
        public virtual void Start()
        {
            SetBuddy();
            SplitDialogue();
            PrepareStep();
            StartGameStep();
            
            GameState = GameState.Tutorial;
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
            // TutorialScene.SetActive(gameState == GameState.Tutorial);
            // PlayingScene.SetActive(gameState == GameState.Playing);
            // WonScene.SetActive(gameState == GameState.Won);
            // LostScene.SetActive(gameState == GameState.Lost);
        }

        /// <summary>
        /// Sets the buddy image based on the player's choice.
        /// </summary>
        protected void SetBuddy()
        {
            BuddyImage.GetComponent<Image>().sprite = DataManager.Buddy switch
            {
                "Cat" => BuddyCatSprite,
                "Dog" => BuddyDogSprite,
                _ => BuddyImage.GetComponent<Image>().sprite,
            };
        }

        /// <summary>
        /// Sets the location file based on the current language.
        /// </summary>
        protected void SetLocationFile()
        {
            LocationFile = DataManager.Language switch
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
        
        /// <summary>
        /// Sets the buddy dialogue text.
        /// </summary>
        /// <param name="dialogue">Text to display in the dialogue popup.</param>
        private void SetBuddyDialogueText(string dialogue)
        {
            BuddyTextBlock.text = dialogue;
            BuddyDialogueObject.SetActive(true);
        }

        /// <summary>
        /// Shows the location file based on the game state.
        /// </summary>
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

        /// <summary>
        /// Updates the dialogue based on the game state.
        /// </summary>
        protected void UpdateDialogue()
        {
            Dialogue dialogue = LocationFile.IsCompleted
                ? GameState == GameState.Won
                    ? WonDialogues.FirstOrDefault(dialogue => dialogue.IsRead != true)
                    : LostDialogues.FirstOrDefault(dialogue => dialogue.IsRead != true)
                : TutorialDialogues.FirstOrDefault(dialogue => dialogue.IsRead != true);
            
            if (dialogue)
            {
                SetBuddyDialogueText(dialogue.Text);
            }
        }
        
        /// <summary>
        /// Returns to the map.
        /// </summary>
        public static void ReturnToMap()
        {
            SceneLoader.LoadScene(GameScene.Navigation);
        }
    }
}