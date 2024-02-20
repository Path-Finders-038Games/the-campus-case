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
        
        public static string GetLocalizedString(string table, string key)
        {
            // Access the localization table collection
            LocalizedStringDatabase tableCollection = LocalizationSettings.StringDatabase;

            // Get the String Table from the collection
            StringTable stringTable = tableCollection.GetTable(table);

            // Get all entries in the table
            ICollection<StringTableEntry> entries = stringTable.Values;

            // Find the entry with the key
            StringTableEntry entry = entries.First(e => e.Key == key);
            
            // Get the localized string
            string localizedString = entry.GetLocalizedString(_locale);
            
            return localizedString;
        }
    }
}