using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public static class LanguageManager
{
    public enum Language
    {
        None,
        Dutch,
        English,
    }

    /// <summary>
    /// Gets the language from the player preferences. Defaults to English.
    /// </summary>
    /// <returns>Currently selected <see cref="Language"/></returns>
    public static Language GetLanguage()
    {
        return PlayerPrefs.GetString("Language") switch
        {
            "NL" => Language.Dutch,
            "EN" => Language.English,
            _ => Language.None,
        };
    }
}