using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public enum Language {Dutch,English }
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
    // Start is called before the first frame update
    void Start()
    {
        _navigationLocation = 1;
        if (PlayerPrefs.GetInt("Currentstep") == 0)
        {
            ContinueBtnEN.SetActive(false);
            ContinueBtnNL.SetActive(false);
        }
    }
    private void LoadLanguage()
    {
        string language = PlayerPrefs.GetString("Language");
        if (language.Equals("NL"))
        {
            OnDutchBtn();
            SwitchToTitleScreen();
        }
        if(language.Equals("EN"))
        {
            OnEnglishBtn();
            SwitchToTitleScreen();
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
    private void ChangeLanguage(Language langauge)
    {
        if (langauge == Language.Dutch)
        {
            SetActiveDutchMenu(true);
            SetActiveEnglishMenu(false);
        }
        if (langauge == Language.English)
        {
            SetActiveDutchMenu(false);
            SetActiveEnglishMenu(true);
        }
    }
    private void SetActiveDutchMenu(bool isActive)
    {
        TitleScreenTextNL.SetActive(isActive);
        BuddyScreenTextNL.SetActive(isActive);
        IntroductionScreenTextNL.SetActive(isActive);
    }
    private void SetActiveEnglishMenu(bool isActive)
    {
        TitleScreenTextEN.SetActive(isActive);
        BuddyScreenTextEN.SetActive(isActive);
        IntroductionScreenTextEN.SetActive(isActive);
    }
    public void OnDutchBtn()
    {
        ChangeLanguage(Language.Dutch);
        PlayerPrefs.SetString("Language", "NL");
        SwitchToTitleScreen();
    }
    public void OnEnglishBtn()
    {
        ChangeLanguage(Language.English);
        PlayerPrefs.SetString("Language", "EN");
        SwitchToTitleScreen();
    }
    private void SwitchToTitleScreen()
    {
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
        BuddyCatImage.SetActive(true);
        BuddyDogImage.SetActive(false);
        SwitchToIntroductionScreen();
    }
    public void OnDogBuddyBtn()
    {
        //save buddy choice
        PlayerPrefs.SetString("Buddy", "Dog");
        BuddyCatImage.SetActive(false);
        BuddyDogImage.SetActive(true);
        SwitchToIntroductionScreen();
    }
}
