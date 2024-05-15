using Dialog;
using Minigames;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//general script where the game logic for the spot the difference minigame is placed
public class SpotTheDifference: Minigame
{
    //button to hide the locationfile
    public Button HideLocationFileButton;

    //material to give the hitbox when it is hit
    public Material material;

    //list of all objects with differences
    public List<GameObject> DifferenceObjects = new List<GameObject>();

    //list of all the difference scripts inside the DifferenceObjects
    private List<Difference> Differences = new List<Difference>();

    private bool _playing {  get; set; }

    private bool _won {  get; set; }

     public override void Start()
    {
        Debug.Log("sadsadcsafc");
        PrepareStep();
        StartGameStep();
        CheckDifference("Bottom-bulbs-Difference");
    }

    // Update is called once per frame
    void Update()
    {
        //Random.Range(0,DifferenceObjects.Count);
        
        //if player input should be registered does something
        if (!_playing) return;
        {
            //checks if the player touched their screen
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
        }
        CheckWin();
    }

    public override void SplitDialogue()
    {
        throw new System.NotImplementedException();
    }

    public override void PrepareStep()
    {
        Debug.Log("sadas");
        _playing = false;
        _won = false;
        foreach (GameObject gameObject in DifferenceObjects)
        {
            Difference difference = gameObject.GetComponent<Difference>();
            Differences.Add(difference);
        }
    }

    public override void StartGameStep()
    {
        ShowLocationFile();
        HideLocationFileButton.onClick.AddListener(HideLocationFile);
    }

    public override void ShowLocationFile()
    {
        _playing = false;
        LocationFileUI.SetActive(true);
    }

    public override void CompleteGameStep()
    {
       //idk
    }
    public override void HideLocationFile()
    {
        LocationFileUI.SetActive(false);
        _playing = true;
    }

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

    public void CheckDifference(string name)
    {
        Debug.Log("1"+name);
        Debug.Log("1.5"+Differences.Count);
        foreach( Difference difference in Differences )
        {
            Debug.Log("2"+difference.name);
            Debug.Log("3"+difference.Original.name);
            Debug.Log("4"+difference.Different.name);
            if ( difference.Original.name == name || difference.Different.name == name )
            {
                ChangeHitboxToCorrect(difference);
                difference.Completed = true;
                Debug.Log(difference.Completed);
                break;
            }
        }
    }

    public void ChangeHitboxToCorrect(Difference difference)
    {

        Collider collider = difference.Original.GetComponent<Collider>();
        Collider collider1 = difference.Different.GetComponent<Collider>();
        Destroy(collider);
        Destroy(collider1);
        MeshRenderer meshRenderer = difference.Original.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    public void CheckWin()
    {
        _won = true;
        foreach( Difference difference in Differences )
        {
            if (!difference.Completed)
            {
                _won = false; break;
            }
        }
    }

    public void Won()
    {
        string Filetext = DialogueManagerV2.GetLocalizedString("LocalizationDialogue", "Differences-Minigame-win");
        string[] separatedStrings = Filetext.Split('-');
        LocationUIName.text = separatedStrings[0];
        LocationUIDescription.text = separatedStrings[1];
        LocationUIFacts.text = "";
        ShowLocationFile();
        HideLocationFileButton.onClick.RemoveAllListeners();
        HideLocationFileButton.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        SceneLoader.LoadScene(GameScene.Navigation);
    }
}
