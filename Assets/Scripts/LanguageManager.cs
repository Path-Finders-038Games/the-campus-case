using UnityEngine;

public static class LanguageManager
{
    //Languages can be added here but must be added in the GetLanguage function (Down below and Minigame.cs (file)
    public enum Language
    {
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
            _ => Language.English,
        };
    }
}