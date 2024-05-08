using UnityEngine.Localization;

public static class LanguageManager
{
    //Languages can be added here but must be added in the GetLanguage function (Down below and Minigame.cs (file)
    public enum Language
    {
        None,
        Dutch,
        English,
    }
    
    /// <summary>
    /// Get the current locale based on the current language setting.
    /// </summary>
    /// <returns><see cref="Locale"/> in the current language setting.</returns>
    public static Locale GetLocale()
    {
        string cultureCode = DataManager.Language switch
        {
            Language.Dutch => "nl",
            Language.English => "en",
            _ => "en",
        };

        return Locale.CreateLocale(cultureCode);
    }
}