using Minigames;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    // parent object of the hangman
    public GameObject HangmanObject;
    //list of seperate parts that make up the hangman
    private List<GameObject> children = new List<GameObject>();

    //list of the parts with added information for the animation
    private List<AnimationTransform> Parts = new List<AnimationTransform>();

    //individual parts
    private GameObject Support_Plank1;
    private GameObject Support_Plank2;
    private GameObject Support_Plank3;
    private GameObject Support_Plank4;
    private GameObject Support_Lower1;
    private GameObject Support_Lower2;
    private GameObject Upper_Plank;
    private GameObject Support_Side1;
    private GameObject Support_Side2;
    private GameObject Rope;
    private GameObject Seat;
    private GameObject Bear;

    //method to set up the hangman object for the start of the minigame
    public void Setup()
    {
        //foreach loop that iterates trough all the children of the hangman object
            foreach (Transform child in HangmanObject.transform)
            {
                    children.Add(child.gameObject);
            }

        //assigns all individual parts to a gameobject
        Support_Plank1 = children[0];
        Support_Plank2 = children[1];
        Support_Plank3 = children[2];
        Support_Plank4 = children[3];
        Support_Lower1 = children[4];
        Support_Lower2 = children[5];
        Upper_Plank = children[6];
        Support_Side1 = children[7];
        Support_Side2 = children[8];
        Rope = children[9];
        Seat = children[10];
        Bear = children[11];


        //adds all gameobjects to the parts list 
        // parts list consists of animationtransform objects
        Parts.Add(new AnimationTransform(Support_Plank1,  new Vector3(-2.15f, 0.5f, 5.8f), new Vector3(0, 90, 0)));
        Parts.Add(new AnimationTransform(Support_Plank2, new Vector3(-2.15f, 0.5f, 4.8f), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Support_Plank3, new Vector3(-2.15f, 0.5f, -4.8f), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Support_Plank4, new Vector3(-2.15f, 0.5f, -5.8f), new Vector3(0, 90, 0)));
        Parts.Add(new AnimationTransform(Support_Lower1, new Vector3(-1, 0.5f, 4), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Support_Lower2, new Vector3(-1, 0.5f, -4), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Upper_Plank, new Vector3(1.7f, 0.5f, 0), new Vector3(0, 0, 0)));
        Parts.Add(new AnimationTransform(Support_Side1, new Vector3(0.6f, 0.5f, 2), new Vector3(0, 0, 90)));
        Parts.Add(new AnimationTransform(Support_Side2, new Vector3(0.6f, 0.5f, -2), new Vector3(0, 0, 90)));
        Parts.Add(new AnimationTransform(Rope, new Vector3(-2.4f, 0.5f, -2.5f), new Vector3(0, 90, 90)));
        Parts.Add(new AnimationTransform(Seat, new Vector3(-4, 0.5f, 2.2f), new Vector3(-90, 0, 0)));
        Parts.Add(new AnimationTransform(Bear, new Vector3(-4.4f, 0.75f, -2.8f), new Vector3(2.4f, -202, 96)));

        //set all parts to their unassembled position
        foreach (AnimationTransform part in Parts)
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
        foreach (AnimationTransform part in Parts)
        {
            if (!part.completed)
            {
                yield return StartCoroutine(part.MoveToPosition());
                yield return StartCoroutine(part.RotateToDirection());
                part.completed = true;
                break;
            }
        }
    }
}

//class containing the part object and information on positions for the animation
public class AnimationTransform
{
    // the part
    public GameObject part;

    //the transform component from the part
    public Transform parttransform;

    //vector 3 objects containing the relative positon and rotation to the parent 
    private Vector3 assembled_pos;
    private Vector3 assembled_rot;

    //vector 3 objects containing the relative target position and rotation for the part to have relative to the parent in the unassembled state
    private Vector3 dissasembled_pos;
    private Vector3 dissasembled_rot;

    //bool to keep track of assembly progress
    public bool completed;

    // constructor for the class
    // rotation is done in EulerAngles to use Vector3 objects
    public AnimationTransform(GameObject Part,Vector3 transform_dissassembled_position,Vector3 transform_dissassembled_rotation)
    {
        part = Part;
        parttransform = part.transform;

        assembled_pos = part.transform.localPosition;
        assembled_rot = part.transform.localEulerAngles;

        dissasembled_pos = transform_dissassembled_position;
        dissasembled_rot = transform_dissassembled_rotation;

        completed = false;
    }

    // method to set the position of the part in dissasembled state
    public IEnumerator SetupPos()
    {

        Vector3 currentPos = parttransform.localPosition;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current position and target position
           parttransform.localPosition = Vector3.Lerp(currentPos, dissasembled_pos, t);
            yield return null;
        }
        //explicit assignment for precision
        parttransform.localPosition = dissasembled_pos;
    }

    //method to set the rotation of the part in the dissassmebled state
    public IEnumerator RotatesSetup()
    {
        Vector3 startRotation = parttransform.localEulerAngles;
        Vector3 finalRotation = dissasembled_rot;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current rotation and target rotation
            parttransform.localEulerAngles = Vector3.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        //explicit assignment for precision
        parttransform.localEulerAngles = finalRotation;
    }

    // method to set the position of the part in asembled state
    public IEnumerator MoveToPosition()
    {
        Vector3 currentPos = parttransform.localPosition;
        float t = 0f; 
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current position and target position
            parttransform.localPosition = Vector3.Lerp(currentPos, assembled_pos, t);
            yield return null;
        }
        //explicit assignment for precision
        parttransform.localPosition = assembled_pos;
    }

    //method to set the rotation of the part in the assmebled state
    public IEnumerator RotateToDirection()
    {
        Vector3 startRotation = parttransform.localEulerAngles;
        Vector3 finalRotation = assembled_rot;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current rotation and target rotation
            parttransform.localEulerAngles = Vector3.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        //explicit assignment for precision
        parttransform.localEulerAngles = finalRotation;
    }

}
