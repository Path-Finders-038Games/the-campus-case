using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Dialog
{
    public static class DialogueManagerV2
    {
        private static readonly List<string> ReadDialogues;

        /// <summary>
        /// Sets the locale to align with the language set in the player preferences.
        /// </summary>
        static DialogueManagerV2()
        {
            ResetLocale();
            
            ReadDialogues = new List<string>();
        }

        public static void ResetLocale()
        {
            LocalizationSettings.SelectedLocale = LanguageManager.GetLocale();
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

            return entry.GetLocalizedString();
        }

        /// <summary>
        /// Gets all localized strings from the given table.
        /// </summary>
        /// <param name="table">Localization table to search in.</param>
        /// <returns>Localized <see cref="string"/> list according to the user preferences.</returns>
        public static List<string> GetAllLocalizedStrings(string table)
        {
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;
            StringTable stringTable = tableCollection.GetTable(table);

            ICollection<StringTableEntry> entries = stringTable.Values;
            return entries.Select(e => e.GetLocalizedString()).ToList();
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
            if (!ReadDialogues.Contains(key))
            {
                ReadDialogues.Add(key);
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
                string localizedString = entry.GetLocalizedString();
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
            return dialogues.FirstOrDefault(dialogue => !ReadDialogues.Contains(dialogue.Text));
        }
    }
}