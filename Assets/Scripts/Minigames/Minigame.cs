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
        Won,
        Lost,
    }

    public abstract class Minigame : MonoBehaviour
    {
        // Buddy dialogue.
        // These will be set in the Unity editor.
        [Header("Buddy Dialogue")] public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;

        // Location info.
        // These will be set in the Unity editor.
        protected LocationFile LocationFile;
        [Header("Location File")] public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        
        // Dialogue lists.
        // These will be set in the minigame itself.
        protected readonly List<Dialogue> TutorialDialogues = new();
        protected readonly List<Dialogue> WonDialogues = new();
        protected readonly List<Dialogue> LostDialogues = new();

        // Game state.
        // This will be set in the minigame itself.
        protected GameState GameState { get; set; }

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
        protected virtual void Start()
        {
            SetBuddy();
            SetLocationFile();
            SplitDialogue();

            GameState = GameState.Tutorial;
        }

        protected abstract void SplitDialogue();
        public abstract void HideLocationFile();
        
        protected abstract void HandleGameOver();
        
        /// <summary>
        /// Shows the location file.
        /// </summary>
        public virtual void ShowLocationFile()
        {
            LocationFileUI.SetActive(true);
        }

        /// <summary>
        /// Sets the buddy image based on the player's choice.
        /// </summary>
        private void SetBuddy()
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
        protected static void ReturnToMap()
        {
            SceneLoader.LoadScene(GameScene.Navigation);
        }
    }
}