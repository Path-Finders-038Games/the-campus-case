using Dialog;
using Navigation;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Language screen
    public GameObject LanguageScreen;

    // Title screen
    public GameObject TitleScreen;
    public GameObject ContinueBtn;
    public TMP_Text ContinueText;
    public TMP_Text NewGameText;
    public TMP_Text QuitText;

    // Buddy screen
    public GameObject BuddyScreen;
    public TMP_Text BuddyChoiceTitle;

    // Introduction screen
    public GameObject IntroductionScreen;
    public TMP_Text IntroductionTitle;
    public TMP_Text IntroductionMessage;
    public TMP_Text IntroductionBuddyMessage;
    public GameObject IntroductionBuddyCat;
    public GameObject IntroductionBuddyDog;

    // Navigation
    private int _navigationLocation;

    private void Start()
    {
        // Load the language based on the player preferences
        LoadLanguage();

        // Set the navigation location to the title screen
        _navigationLocation = 1;

        // If there is no saved game, hide the continue button
        if (DataManager.CurrentStep == 0)
        {
            ContinueBtn.SetActive(false);
        }
    }

    /// <summary>
    /// This method sets the language based on the player preferences.
    /// If there is no language set, show the language screen.
    /// </summary>
    private void LoadLanguage()
    {
        switch (LanguageManager.GetLanguage())
        {
            case LanguageManager.Language.Dutch:
                SetLanguageToDutch();
                break;
            case LanguageManager.Language.English:
                SetLanguageToEnglish();
                break;
            case LanguageManager.Language.None:
            default:
                LanguageScreen.SetActive(true);
                TitleScreen.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Initialize a new game and switch to the buddy selection screen.
    /// </summary>
    public void OnNewGameBtn()
    {
        DataManager.CurrentStep = 0;
        DataManager.CurrentMap = "C0Map";
        TitleScreen.SetActive(false);
        BuddyScreen.SetActive(true);
    }

    /// <summary>
    /// Switch from the total screen to navigation screen.
    /// This is where the user left off.
    /// </summary>
    public void OnContinueGameBtn()
    {
        TitleScreen.SetActive(false);
        SwitchToNavigation();
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Set the language to Dutch and switch to the title screen.
    /// This is a separate method to allow for the language to be set from the language screen.
    /// </summary>
    public void SetLanguageToDutch()
    {
        PlayerPrefs.SetString("Language", "NL");
        SwitchToTitleScreen();
    }

    /// <summary>
    /// Set the language to English and switch to the title screen.
    /// This is a separate method to allow for the language to be set from the language screen.
    /// </summary>
    public void SetLanguageToEnglish()
    {
        PlayerPrefs.SetString("Language", "EN");
        SwitchToTitleScreen();
    }

    /// <summary>
    /// Switch from the language screen to the title screen.
    /// </summary>
    private void SwitchToTitleScreen()
    {
        PopulateUiWithLocalizedStrings();
        LanguageScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    /// <summary>
    /// Switch to the navigation screen.
    /// This is where the user left off.
    /// </summary>
    public void SwitchToNavigation()
    {
        SceneManager.LoadScene(_navigationLocation);
    }

    /// <summary>
    /// Switch from the title screen to the buddy selection screen.
    /// </summary>
    private void SwitchToIntroductionScreen()
    {
        BuddyScreen.SetActive(false);
        IntroductionScreen.SetActive(true);
    }

    /// <summary>
    /// Set buddy to cat and switch to the introduction screen.
    /// This is a separate method to allow for the buddy to be set from the buddy screen.
    /// </summary>
    public void SetBuddyToCat()
    {
        PlayerPrefs.SetString("Buddy", "Cat");
        IntroductionBuddyCat.SetActive(true);
        IntroductionBuddyDog.SetActive(false);
        SwitchToIntroductionScreen();
    }

    /// <summary>
    /// Set buddy to dog and switch to the introduction screen.
    /// This is a separate method to allow for the buddy to be set from the buddy screen.
    /// </summary>
    public void SetBuddyToDog()
    {
        PlayerPrefs.SetString("Buddy", "Dog");
        IntroductionBuddyCat.SetActive(false);
        IntroductionBuddyDog.SetActive(true);
        SwitchToIntroductionScreen();
    }

    /// <summary>
    /// Populates the localized strings for the UI elements.
    /// </summary>
    private void PopulateUiWithLocalizedStrings()
    {
        // Title screen
        ContinueText.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "continueBtn");
        NewGameText.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "newGameBtn");
        QuitText.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "quitBtn");

        // Buddy screen
        BuddyChoiceTitle.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "buddyChoiceTitle");

        // Introduction screen
        IntroductionTitle.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionTitle");
        IntroductionMessage.text = DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionMessage");
        IntroductionBuddyMessage.text =
            DialogueManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionBuddyMessage");
    }
}