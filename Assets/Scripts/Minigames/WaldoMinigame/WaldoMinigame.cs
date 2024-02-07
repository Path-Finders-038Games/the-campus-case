using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaldoMinigame : Minigame
{
    public GameObject Target;
    public LocationInfoScriptableObject LocationInfo;
    private bool _isChecking;
    public TMP_Text LocationUIName;
    public TMP_Text LocationUIDescription;
    public TMP_Text LocationUIFacts;
    public TMP_Text LocationUIHintNextLocation;
    public GameObject LocationFileUI;
    public GameObject GameBoard;
    public Button HideLocationFileButton;
    public TMP_Text BuddyTextBlock;
    public GameObject BuddyDialogueObject;
    public GameObject BuddyImage;
    public Sprite BuddyDogSprite;
    public Sprite BuddyCatSprite;
    private List<Dialogue> _startMinigame = new List<Dialogue>();
    private List<Dialogue> _endMinigame = new List<Dialogue>();
    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateDialogue();
        if (_isChecking)
        {
            Debug.Log(Input.touchCount);
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RacastCheck();
            }
        }
    }
    public override void SplitDialogue()
    {
        if (PlayerPrefs.GetString("Language").Equals("NL"))
        {
            _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-3][0]);
            _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-3][1]);
            _endMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-3][2]);
        }
        if (PlayerPrefs.GetString("Language").Equals("EN"))
        {
            _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-3][0]);
            _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-3][1]);
            _endMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-3][2]);
        }
    }
    public override void PrepareStep()
    {
        _isChecking = false;
        SetLocationFile();
        HideLocationFileButton.onClick.AddListener(HideLocationFile);
    }

    public override void StartGameStep()
    {
        ShowLocationFile();
    }
    public override void CompleteGameStep()
    {
        _isChecking = false;
        LocationUIHintNextLocation.text = "Hint for next location \n"+LocationFile.HintNextLocation;
        LocationFile.IsCompleted = true;
        ShowLocationFile();
        GameBoard.SetActive(false);
    }
    public override void SetLocationFile()
    {
        if (PlayerPrefs.GetString("Language").Equals("NL"))
        {
            LocationFile = DutchFile();
        }
        else
        {
            LocationFile = EnglishFile();
        }
        LocationUIName.text = LocationFile.Name;
        LocationUIDescription.text = LocationFile.Description;
        StringBuilder locationFacts = new StringBuilder("Facts\n");
        foreach (string fact in LocationFile.Facts)
        {
            locationFacts.AppendLine(fact + "\n");
        }
        LocationUIFacts.text = locationFacts.ToString();
    }

    public override LocationFile DutchFile()
    {
        return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description, LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);

    }

    public override LocationFile EnglishFile()
    {
        return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description, LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);

    }
    public override void ShowLocationFile()
    {
        _isChecking = false;
        LocationFileUI.SetActive(true);
    }
    public override void HideLocationFile()
    {
        LocationFileUI.SetActive(false);
        _isChecking = true;
        if (LocationFile.IsCompleted)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void RacastCheck()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit))
        {
            string name = hit.transform.gameObject.name;
            if (name.Equals(Target.name))
            {
                CompleteGameStep();
            }
        }
    }
    public override void SetBuddy()
    {
        string buddyChoice = PlayerPrefs.GetString("Buddy");
        if (buddyChoice.Equals("Cat"))
        {
            BuddyImage.GetComponent<Image>().sprite = BuddyCatSprite;
        }
        if (buddyChoice.Equals("Dog"))
        {
            BuddyImage.GetComponent<Image>().sprite = BuddyDogSprite;
        }
    }
    public override void SetBuddyDialogueText(string Dialogue)
    {
        BuddyTextBlock.text = Dialogue;
        BuddyDialogueObject.SetActive(true);
    }
    public override void OnTextBlockClick()
    {
        string text = BuddyTextBlock.text;
        if (!LocationFile.IsCompleted)
        {
            _startMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
        }
        else
        {
            _endMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
        }
        BuddyDialogueObject.SetActive(false);
    }
    public override void UpdateDialogue()
    {
        if (!LocationFile.IsCompleted)
        {
            foreach (Dialogue dialogue in _startMinigame)
            {
                if (dialogue.IsRead != true)
                {
                    SetBuddyDialogueText(dialogue.Text);
                    break;
                }
            }
        }
        else
        {
            foreach (Dialogue dialogue in _endMinigame)
            {
                if (dialogue.IsRead != true)
                {
                    SetBuddyDialogueText(dialogue.Text);
                    break;
                }
            }
        }

    }
}
