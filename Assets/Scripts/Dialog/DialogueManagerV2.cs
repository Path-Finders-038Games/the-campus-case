using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Dialog
{
    public static class DialogueManagerV2
    {
        private static Locale _locale;
        private static readonly Dictionary<string, int> DialogueProgress = new(); // Key: map/minigame name, Value: current step

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
        /// Resets all dialogue progress.
        /// </summary>
        public static void ResetDialogueProgress()
        {
            DialogueProgress.Clear();
        }

        /// <summary>
        /// Resets the dialogue progress for the given map.
        /// </summary>
        /// <param name="map"></param>
        public static void ResetDialogueProgress(string map)
        {
            DialogueProgress[map] = 0;
        }
    }
}