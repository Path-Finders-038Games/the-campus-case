using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Dialog
{
    public static class DialogueManagerV2
    {
        private static Locale _locale;
        private static List<string> _readDialogues;

        /// <summary>
        /// Sets the locale to align with the language set in the player preferences.
        /// </summary>
        public static void Initialize()
        {
            string cultureCode = LanguageManager.GetLanguage() switch
            {
                LanguageManager.Language.Dutch => "nl",
                LanguageManager.Language.English => "en",
                _ => "en",
            };

            _locale = Locale.CreateLocale(cultureCode);
            LocalizationSettings.SelectedLocale = _locale;

            _readDialogues = new List<string>();
        }

        /// <summary>
        /// Gets the string from the localization table based on the key and table.
        /// Automatically selects the correct language based on the player preferences.
        /// </summary>
        /// <param name="table">Localization table to search in.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>Localized <see cref="string"/> according to the user preferences.</returns>
        public static string GetLocalizedString(string table, string key)
        {
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;
            StringTable stringTable = tableCollection.GetTable(table);

            ICollection<StringTableEntry> entries = stringTable.Values;
            StringTableEntry entry = entries.First(e => e.Key == key);

            return entry.GetLocalizedString(_locale);
        }

        /// <summary>
        /// Gets the dialogue for the given map and key.
        /// </summary>
        /// <param name="table">Localization table to search in.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>Localized <see cref="Dialogue"/> according to the user preferences.</returns>
        public static Dialogue GetDialogue(string table, string key)
        {
            string localizedString = GetLocalizedString(table, key);
            Dialogue dialogue = new(localizedString);
            return dialogue;
        }

        /// <summary>
        /// Marks the dialogue as read.
        /// </summary>
        /// <param name="key">Key of the dialogue to mark as read.</param>
        public static void MarkDialogueAsRead(string key)
        {
            if (!_readDialogues.Contains(key))
            {
                _readDialogues.Add(key);
            }
        }

        /// <summary>
        /// Gets the dialogues starting with the given key from the given table.
        /// </summary>
        /// <param name="table">Localization table to search in.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>List of Dialogues starting with the given key.</returns>
        public static List<Dialogue> GetDialoguesStartingWith(string table, string key)
        {
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;
            StringTable stringTable = tableCollection.GetTable(table);

            ICollection<StringTableEntry> entries = stringTable.Values;
            List<Dialogue> dialogues = new();

            foreach (StringTableEntry entry in entries.Where(e => e.Key.StartsWith(key)))
            {
                string localizedString = entry.GetLocalizedString(_locale);
                Dialogue dialogue = new(localizedString);
                dialogues.Add(dialogue);
            }

            return dialogues;
        }

        /// <summary>
        /// Gets the first unread dialogue from the given list of dialogues.
        /// </summary>
        /// <param name="dialogues">IEnumerable with dialogues to ve checked.</param>
        /// <returns>First unread Dialogue</returns>
        public static Dialogue GetFirstUnreadDialogue(IEnumerable<Dialogue> dialogues)
        {
            return dialogues.FirstOrDefault(dialogue => !_readDialogues.Contains(dialogue.Text));
        }
    }
}