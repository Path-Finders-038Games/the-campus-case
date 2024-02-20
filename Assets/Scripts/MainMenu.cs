using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject BuddyScreen;
    public GameObject LanguageScreen;
    public GameObject IntroductionScreen;
    public GameObject TitleScreenTextEN;
    public GameObject TitleScreenTextNL;
    public GameObject BuddyScreenTextEN;
    public GameObject BuddyScreenTextNL;
    public GameObject IntroductionScreenTextEN;
    public GameObject IntroductionScreenTextNL;
    public GameObject ContinueBtnNL;
    public GameObject ContinueBtnEN;
    public GameObject BuddyCatImage;
    public GameObject BuddyDogImage;
    private int _navigationLocation;

    void Awake()
    {
        LoadLanguage();
    }

    void Start()
    {
        _navigationLocation = 1;

        // If there is no saved game, hide the continue button
        if (PlayerPrefs.GetInt("Currentstep") == 0)
        {
            // Two buttons for each language.
            // TODO: Add localisation to reduce the amount of buttons
            ContinueBtnEN.SetActive(false);
            ContinueBtnNL.SetActive(false);
        }
    }

    /// <summary>
    /// This method sets the language based on the player preferences.
    /// </summary>
    private void LoadLanguage()
    {
        switch (Language.GetLanguage())
        {
            case Language.Languages.Dutch:
                SetLanguageToDutch();
                break;
            case Language.Languages.English:
            default:
                SetLanguageToEnglish();
                break;
        }
    }

    /// <summary>
    /// Initialize a new game and switch to the buddy selection screen.
    /// </summary>
    public void OnNewGameBtn()
    {
        PlayerPrefs.SetInt("Currentstep", 0);
        PlayerPrefs.SetString("Currentmap", "C0Map");
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
    /// This method changes the language of the game.
    /// It does *NOT* save the language to the player preferences.
    /// </summary>
    /// <param name="language"><see cref="Language"/> to be switched to.</param>
    private void ChangeLanguage(Language.Languages language)
    {
        switch (language)
        {
            case Language.Languages.Dutch:
                SetActiveDutchMenu(true);
                SetActiveEnglishMenu(false);
                break;
            case Language.Languages.English:
            default:
                SetActiveDutchMenu(false);
                SetActiveEnglishMenu(true);
                break;
        }
    }

    /// <summary>
    /// Set the active state of the Dutch menu.
    /// </summary>
    /// <param name="isActive"><see cref="bool"/> to set the state</param>
    private void SetActiveDutchMenu(bool isActive)
    {
        TitleScreenTextNL.SetActive(isActive);
        BuddyScreenTextNL.SetActive(isActive);
        IntroductionScreenTextNL.SetActive(isActive);
    }

    /// <summary>
    /// Set the active state of the English menu.
    /// </summary>
    /// <param name="isActive"><see cref="bool"/> to set the state</param>
    private void SetActiveEnglishMenu(bool isActive)
    {
        TitleScreenTextEN.SetActive(isActive);
        BuddyScreenTextEN.SetActive(isActive);
        IntroductionScreenTextEN.SetActive(isActive);
    }

    /// <summary>
    /// Set the language to Dutch and switch to the title screen.
    /// This is a separate method to allow for the language to be set from the language screen.
    /// </summary>
    public void SetLanguageToDutch()
    {
        ChangeLanguage(Language.Languages.Dutch);
        PlayerPrefs.SetString("Language", "NL");
        SwitchToTitleScreen();
    }

    /// <summary>
    /// Set the language to English and switch to the title screen.
    /// This is a separate method to allow for the language to be set from the language screen.
    /// </summary>
    public void SetLanguageToEnglish()
    {
        ChangeLanguage(Language.Languages.English);
        PlayerPrefs.SetString("Language", "EN");
        SwitchToTitleScreen();
    }

    /// <summary>
    /// Switch from the language screen to the title screen.
    /// </summary>
    private void SwitchToTitleScreen()
    {
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
        BuddyCatImage.SetActive(true);
        BuddyDogImage.SetActive(false);
        SwitchToIntroductionScreen();
    }

    /// <summary>
    /// Set buddy to dog and switch to the introduction screen.
    /// This is a separate method to allow for the buddy to be set from the buddy screen.
    /// </summary>
    public void SetBuddyToDog()
    {
        PlayerPrefs.SetString("Buddy", "Dog");
        BuddyCatImage.SetActive(false);
        BuddyDogImage.SetActive(true);
        SwitchToIntroductionScreen();
    }
}