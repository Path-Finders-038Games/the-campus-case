using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 TODO:
Optimize code for setting position and rotation
fix weird rotation
make it move and rotate simultanious
 */

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // parent object of the hangman
    public GameObject HangmanObject;
    //list of seperate parts that make up the hangman
    private List<GameObject> _children = new List<GameObject>();

    //list of the parts with added information for the animation
    private List<AnimationTransform> _parts = new List<AnimationTransform>();

    //individual parts
    private GameObject _support_Plank1;
    private GameObject _support_Plank2;
    private GameObject _support_Plank3;
    private GameObject _support_Plank4;
    private GameObject _support_Lower1;
    private GameObject _support_Lower2;
    private GameObject _upper_Plank;
    private GameObject _support_Side1;
    private GameObject _support_Side2;
    private GameObject _rope;
    private GameObject _seat;
    private GameObject _bear;

    //method to set up the hangman object for the start of the minigame
    public void Setup()
    {
        //foreach loop that iterates trough all the children of the hangman object
            foreach (Transform child in HangmanObject.transform)
            {
                    _children.Add(child.gameObject);
            }

        //assigns all individual parts to a gameobject
        _support_Plank1 = _children[0];
        _support_Plank2 = _children[1];
        _support_Plank3 = _children[2];
        _support_Plank4 = _children[3];
        _support_Lower1 = _children[4];
        _support_Lower2 = _children[5];
        _upper_Plank = _children[6];
        _support_Side1 = _children[7];
        _support_Side2 = _children[8];
        _rope = _children[9];
        _seat = _children[10];
        _bear = _children[11];


        //adds all gameobjects to the parts list 
        // parts list consists of animationtransform objects
        _parts.Add(new AnimationTransform(_support_Plank1,  new Vector3(-2.15f, 0.5f, 5.8f), new Vector3(0, 90, 0)));
        _parts.Add(new AnimationTransform(_support_Plank2, new Vector3(-2.15f, 0.5f, 4.8f), new Vector3(0, -90, 0)));
        _parts.Add(new AnimationTransform(_support_Plank3, new Vector3(-2.15f, 0.5f, -4.8f), new Vector3(0, -90, 0)));
        _parts.Add(new AnimationTransform(_support_Plank4, new Vector3(-2.15f, 0.5f, -5.8f), new Vector3(0, 90, 0)));
        _parts.Add(new AnimationTransform(_support_Lower1, new Vector3(-1, 0.5f, 4), new Vector3(0, -90, 0)));
        _parts.Add(new AnimationTransform(_support_Lower2, new Vector3(-1, 0.5f, -4), new Vector3(0, -90, 0)));
        _parts.Add(new AnimationTransform(_upper_Plank, new Vector3(1.7f, 0.5f, 0), new Vector3(0, 0, 0)));
        _parts.Add(new AnimationTransform(_support_Side1, new Vector3(0.6f, 0.5f, 2), new Vector3(0, 0, 90)));
        _parts.Add(new AnimationTransform(_support_Side2, new Vector3(0.6f, 0.5f, -2), new Vector3(0, 0, 90)));
        _parts.Add(new AnimationTransform(_rope, new Vector3(-2.4f, 0.5f, -2.5f), new Vector3(0, 90, 90)));
        _parts.Add(new AnimationTransform(_seat, new Vector3(-4, 0.5f, 2.2f), new Vector3(-90, 0, 0)));
        _parts.Add(new AnimationTransform(_bear, new Vector3(-4.4f, 0.75f, -2.8f), new Vector3(2.4f, -202, 96)));

        //set all parts to their unassembled position
        foreach (AnimationTransform part in _parts)
        {
            StartCoroutine(part.SetupPos());
            StartCoroutine(part.RotatesSetup());
        }
    }
    //method to start playing the assembly animation
    public void AssemblePart()
    {
        StartCoroutine(AnimationPlay());
    }
    //coroutine to assemble the first part that hasnt yet been assembled
    public IEnumerator AnimationPlay()
    {
        foreach (AnimationTransform part in _parts)
        {
            if (!part.Completed)
            {
                part.Completed = true;
                yield return StartCoroutine(part.MoveToPosition());
                yield return StartCoroutine(part.RotateToDirection());
                break;
            }
        }
    }
}
