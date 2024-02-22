using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Dialog
{
    public class DialogManagerV2 : MonoBehaviour
    {
        private static Locale _locale;
        
        /// <summary>
        /// Sets the locale to align with the language set in the player preferences.
        /// </summary>
        public static void Initialize()
        {
            Debug.Log("Initializing DialogManagerV2...");

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
    }
}