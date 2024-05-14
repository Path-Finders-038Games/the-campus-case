using Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//general script where the game logic for the spot the difference minigame is placed
public class SpotTheDifference: Minigame
{
    public GameObject Original { get; set; }
    public GameObject Different { get; set; }

    //button to hide the locationfile
    public Button HideLocationFileButton;

    public GameObject OriginalHourglas { get; set; }

    public GameObject DifferentHourglas { get; set; }

    public List<Difference> differences = new List<Difference>();

    private bool _playing {  get; set; }

    private bool _won {  get; set; }

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
        CheckWin();
    }

    public override void SplitDialogue()
    {
        throw new System.NotImplementedException();
    }

    public override void PrepareStep()
    {
        _playing = false;
        _won = false;
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
        throw new System.NotImplementedException();
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
        foreach( Difference difference in differences )
        {
            if ( difference.Original.name == name || difference.Different.name == name )
            {
                difference.Completed = true; break;
            }
        }
    }

    public void CheckWin()
    {
        _won = true;
        foreach( Difference difference in differences )
        {
            if (!difference.Completed)
            {
                _won = false; break;
            }
        }
    }

    public void Won()
    {

        ShowLocationFile();
        HideLocationFileButton.onClick.RemoveAllListeners();
        HideLocationFileButton.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        SceneLoader.LoadScene(GameScene.Navigation);
    }
}
