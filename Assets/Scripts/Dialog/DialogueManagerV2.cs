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
        /// Returns the current step for the given map. After the first call, any subsequent call will return the next dialogue step.
        /// If the last step has been reached, it will return the last step again.
        /// </summary>
        /// <param name="table">Localization table to look in for the maps/minigames dialog</param>
        /// <param name="map">Map/Minigame to get the dialog for.</param>
        /// <returns>Localized dialog <see cref="string"/> according to the user preferences.</returns>
        /// <example>
        /// <code>
        /// string dialog = DialogueManagerV2.GetDialogueString("SlidingPuzzle", "SlidingPuzzleExplanation");
        /// </code>
        /// This will return the first step of the SlidingPuzzleExplanation dialog.
        /// (SlidingPuzzleExplanation_0 in the localization table)
        /// <code>
        /// string dialog = DialogueManagerV2.GetDialogueString("SlidingPuzzle", "SlidingPuzzleExplanation");
        /// </code>
        /// This will return the second step of the SlidingPuzzleExplanation dialog.
        /// (SlidingPuzzleExplanation_1 in the localization table)
        /// </example>
        public static string GetDialogueString(string table, string map)
        {
            // Get the string table from the localization settings
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;
            StringTable stringTable = tableCollection.GetTable(table);
            ICollection<StringTableEntry> entries = stringTable.Values;
            
            // If the map is not in the dictionary, add it with a value of 0
            if (!DialogueProgress.ContainsKey(map)) DialogueProgress.Add(map, 0);

            // Get the current step
            int step = DialogueProgress[map];
            
            // count the amount of steps in the dialog
            int stepCount = entries.Count(e => e.Key.StartsWith(map + "_"));
            
            // If the current step is greater than or equal to the amount of steps, return the last step
            if (step >= stepCount) step = stepCount - 1;
            
            // Create the key
            string key = map + "_" + step;

            // Get the entry from the string table
            StringTableEntry entry = entries.First(e => e.Key == key);
            
            // Increase the step
            DialogueProgress[map]++;
            
            // Return the localized string
            return entry.GetLocalizedString(_locale);
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