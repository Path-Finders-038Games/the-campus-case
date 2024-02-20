using UnityEngine;

public static class Language
{
    public enum Languages
    {
        Dutch,
        English,
    }
    
    /// <summary>
    /// Gets the language from the player preferences. Defaults to English.
    /// </summary>
    /// <returns>Currently selected <see cref="Language"/></returns>
    public static Languages GetLanguage()
    {
        return PlayerPrefs.GetString("Language") switch
        {
            "NL" => Languages.Dutch,
            "EN" => Languages.English,
            _ => Languages.English
        };
    }
}

