using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    void Awake()
    {
        LoadLanguage();
    }
    // Start is called before the first frame update
    void Start()
    {
        _navigationLocation = 1;
        if (PlayerPrefs.GetInt("Currentstep") == 0)
        {
            ContinueBtn.SetActive(false);
        }
    }
    private void LoadLanguage()
    {
        switch (LanguageManager.GetLanguage())
        {
            case LanguageManager.Language.Dutch:
                OnDutchBtn();
                break;
            case LanguageManager.Language.English:
                OnEnglishBtn();
                break;
            case LanguageManager.Language.None:
            default:
                LanguageScreen.SetActive(true);
                TitleScreen.SetActive(false);
                break;
        }
        
    }
    public void OnNewGameBtn()
    {
        PlayerPrefs.SetInt("Currentstep", 0);
        PlayerPrefs.SetString("Currentmap", "C0Map");
        TitleScreen.SetActive(false);
        BuddyScreen.SetActive(true);
    }
    public void OnContinueGameBtn()
    {
        TitleScreen.SetActive(false);
        SwitchToNavigation();
    }
    public void OnQuitBtn()
    {
        Application.Quit();
    }
    public void OnDutchBtn()
    {
        PlayerPrefs.SetString("Language", "NL");
        SwitchToTitleScreen();
    }
    public void OnEnglishBtn()
    {
        PlayerPrefs.SetString("Language", "EN");
        SwitchToTitleScreen();
    }
    private void SwitchToTitleScreen()
    {
        DialogManagerV2.Initialize();
        PopulateUiWithLocalizedStrings();
        LanguageScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }
    public void SwitchToNavigation()
    {
        //go to navigation scene
        SceneManager.LoadScene(_navigationLocation);
    }
    private void SwitchToIntroductionScreen()
    {
        BuddyScreen.SetActive(false);
        IntroductionScreen.SetActive(true);
    }
    public void OnCatBuddyBtn()
    {
        //save buddy choice
        PlayerPrefs.SetString("Buddy", "Cat");
        IntroductionBuddyCat.SetActive(true);
        IntroductionBuddyDog.SetActive(false);
        SwitchToIntroductionScreen();
    }
    public void OnDogBuddyBtn()
    {
        //save buddy choice
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
        ContinueText.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "continueBtn");
        NewGameText.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "newGameBtn");
        QuitText.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "quitBtn");
        
        // Buddy screen
        BuddyChoiceTitle.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "buddyChoiceTitle");
        
        // Introduction screen
        IntroductionTitle.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionTitle");
        IntroductionMessage.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionMessage");
        IntroductionBuddyMessage.text = DialogManagerV2.GetLocalizedString("LocalizationMainMenu", "introductionBuddyMessage");
    }
}
