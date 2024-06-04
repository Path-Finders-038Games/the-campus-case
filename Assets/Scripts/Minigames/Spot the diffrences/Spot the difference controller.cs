using Dialog;
using Minigames;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//todo:
//code for storing when you won
//fix size
//notify when a wrong guess is made 

//general script where the game logic for the spot the difference minigame is placed
public class SpotTheDifference: Minigame
{
    //button to hide the locationfile
    public Button HideLocationFileButton;

    //material to give the hitbox when it is hit
    public Material GuessedMaterial;

    public TMP_Text ScoreText;

    //list of all objects with differences
    public List<GameObject> DifferenceObjects = new List<GameObject>();

    //list of all the difference scripts inside the DifferenceObjects
    private List<Difference> _differences = new List<Difference>();

    //bool to check whether the game is in play
    private bool _playing {  get; set; }

    //bool to check if the user satisfies the win condition
    private bool _won {  get; set; }

    //use start method from general minigame class
     public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {   
        //if player input should be registered does something
        if (!_playing) return;
        {
            //checks if the player touched their screen
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
        }
        DifferenceRemaining();
        CheckWin();
    }

    //not needed method from parent class
    public override void SplitDialogue()
    {
        Debug.Log("");  
    }

    // method  to prepare the game for playing by gathering the required objects and setting them to the correct state
    public override void PrepareStep()
    {
        _playing = false;
        _won = false;
        foreach (GameObject gameObject in DifferenceObjects)
        {
            Difference difference = gameObject.GetComponent<Difference>();
            _differences.Add(difference);
            MeshRenderer meshRenderer1 = difference.Original.GetComponent<MeshRenderer>();
            MeshRenderer meshRenderer2= difference.Different.GetComponent<MeshRenderer>();
            meshRenderer1.enabled = false;
            meshRenderer2.enabled = false;
        }
    }

    //method to start playing the game
    public override void StartGameStep()
    {
        ShowLocationFile();
        HideLocationFileButton.onClick.AddListener(HideLocationFile);
    }

    //method to display the file overlay
    public override void ShowLocationFile()
    {
        _playing = false;
        LocationFileUI.SetActive(true);
    }

    // method to complete the game and return to the map
    public override void CompleteGameStep()
    {
        SceneLoader.LoadScene(GameScene.Navigation);
    }

    //method to hide the file overlay
    public override void HideLocationFile()
    {
        LocationFileUI.SetActive(false);
        _playing = true;
    }

    //method to check what you pressed using raycast
    public void RaycastCheck()
    {
        RaycastHit hit;
        Vector2 touchpos = Input.touches[0].position;
        Ray ray = Camera.main.ScreenPointToRay(touchpos);
        if (Physics.Raycast(ray, out hit, 100))
        {
            string name = hit.transform.gameObject.name;
            CheckDifference(name);
        }
    }

    //method to check the item you hit and if it is one of the differences
    public void CheckDifference(string name)
    {
        foreach( Difference difference in _differences )
        {
            if ( difference.Original.name == name || difference.Different.name == name )
            {
                ChangeHitboxToCorrect(difference);
                difference.Completed = true;
                break;
            }
        }
    }

    //change the material of a difference object to a transparant green material
    public void ChangeHitboxToCorrect(Difference difference)
    {

        Collider collider = difference.Original.GetComponent<Collider>();
        Collider collider1 = difference.Different.GetComponent<Collider>();
        Destroy(collider);
        Destroy(collider1);
        MeshRenderer meshRenderer = difference.Original.GetComponent<MeshRenderer>();
        MeshRenderer meshRenderer1 = difference.Different.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        meshRenderer.material = GuessedMaterial;
        meshRenderer1.enabled = true;
        meshRenderer1.material = GuessedMaterial;
    }

    //check if the user has won
    public void CheckWin()
    {
        _won = true;

        foreach( Difference difference in _differences )
        {
            if (difference.Completed == false)
            {
                _won = false; break;
            }
        }
        if (_won)
        {
            Won();
        }
        
    }

    //method to change the file overlay text and display it
    public void Won()
    {
        string Filetext = DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "Differences-Minigame-win");
        string[] separatedStrings = Filetext.Split('-');
        LocationUIName.text = separatedStrings[0];
        LocationUIDescription.text = separatedStrings[1];
        LocationUIFacts.text = "";
        ShowLocationFile();
        HideLocationFileButton.onClick.RemoveAllListeners();
        HideLocationFileButton.onClick.AddListener(CompleteGameStep);
    }

    //method to update and show the amount of differences remaining to the player
    public void DifferenceRemaining()
    {
        int count = 0;
        foreach (Difference difference in _differences)
        {
            if (difference.Completed == false)
            {
                count++;
            }
        }
        string Filetext = DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "Differences-Minigame-counter");
        string Textbox = Filetext + ": "+count.ToString();
        ScoreText.text = Textbox;
    }
}
