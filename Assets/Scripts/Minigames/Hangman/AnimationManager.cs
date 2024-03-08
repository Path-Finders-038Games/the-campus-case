using Minigames;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //children = HangmanObject.GetChildGameObjects();
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject HangmanObject;
    private List<GameObject> children = new List<GameObject>();
    private int progress = 0;

    private List<AnimationTransform> Parts = new List<AnimationTransform>();

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

    public void Setup()
    {
            foreach (Transform child in HangmanObject.transform)
            {
                    children.Add(child.gameObject);
            }
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


        Debug.Log("children count = " + children.Count);



        Parts.Add(new AnimationTransform(Support_Plank1, Support_Plank1.transform,new Vector3(7,0.5f,1),new Vector3(0,0,0)));
        Parts.Add(new AnimationTransform(Support_Plank2, Support_Plank2.transform, new Vector3(5.75f, 0.5f, 1), new Vector3(0, 0, 0)));
        Parts.Add(new AnimationTransform(Support_Plank3, Support_Plank3.transform, new Vector3(3.7f, 0.5f, 5.6f), new Vector3(0, 90, 0)));
        Parts.Add(new AnimationTransform(Support_Plank4, Support_Plank4.transform, new Vector3(3.7f, 0.5f, -4.3f), new Vector3(0, 90, 0)));
        Parts.Add(new AnimationTransform(Support_Lower1, Support_Lower1.transform, new Vector3(2.1f, 0.5f, -2.2f), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Support_Lower2, Support_Lower2.transform, new Vector3(2.1f, 0.5f, -3), new Vector3(0, -90, 0)));
        Parts.Add(new AnimationTransform(Upper_Plank, Upper_Plank.transform, new Vector3(-1.3f, 0.5f, -2.9f), new Vector3(0, 0, 0)));
        Parts.Add(new AnimationTransform(Support_Side1, Support_Side1.transform, new Vector3(-2.7f, 0.5f, -4.2f), new Vector3(0, 0, 0)));
        Parts.Add(new AnimationTransform(Support_Side2, Support_Side2.transform, new Vector3(-2.7f, 0.5f, -0.8f), new Vector3(0, 0, 0)));
        Parts.Add(new AnimationTransform(Rope, Rope.transform, new Vector3(2.5f, 0.5f, -1), new Vector3(0, 90, 90)));
        Parts.Add(new AnimationTransform(Seat, Seat.transform, new Vector3(0.75f, 0.5f, -0.3f), new Vector3(-90, 0, 0)));
        Parts.Add(new AnimationTransform(Bear, Bear.transform, new Vector3(-0.72f, 0.27f, 4.34f), new Vector3(0, -65, 100)));


        Debug.Log("parts count = " + Parts.Count);

        foreach (var part in Parts)
        {
            part.setuppos();
            part.rotatesetup();
        }

        foreach(var part in Parts)
        {
            AnimationPlay();
        }
    }

    public void AnimationPlay()
    {
        foreach(var part in Parts)
        {
            if(part.completed == false)
            {
                part.MoveToPosition();
                part.RotateToDirection();
                part.completed = true;
                Debug.Log(" play that shit");
            }
            break;
        }
    }
    
    
}

public class AnimationTransform
{
    private GameObject part;
    private Transform assembled;
    private Transform dissasembled;
    public bool completed;

    public AnimationTransform(GameObject Part,Transform transform_assembled,Vector3 transform_dissassembled_position,Vector3 transform_dissassembled_rotation)
    {
        part = Part;
        assembled = transform_assembled;
        assembled.localPosition = transform_assembled.localPosition;
        dissasembled = transform_assembled;
        dissasembled.localPosition = transform_dissassembled_position;
        completed = false;
    }

    public IEnumerator setuppos()
    {
        var currentPos = part.transform.localPosition;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1;
            part.transform.localPosition = Vector3.Lerp(currentPos, dissasembled.localPosition, t);
            yield return null;
        }
        part.transform.localPosition = dissasembled.localPosition;
    }

    public IEnumerator rotatesetup()
    {
        var startRotation = part.transform.rotation;
        var direction = dissasembled.transform.localPosition - part.transform.localPosition;
        var finalRotation = Quaternion.LookRotation(direction);
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1;
            part.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        part.transform.localRotation = finalRotation;
    }

    public IEnumerator MoveToPosition()
    {
        var currentPos = part.transform.localPosition;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 3;
            part.transform.localPosition = Vector3.Lerp(currentPos, assembled.localPosition, t);
            yield return null;
        }
        part.transform.localPosition = assembled.localPosition;
    }

    public IEnumerator RotateToDirection()
    {
        var startRotation = part.transform.localRotation;
        var direction = assembled.transform.localPosition - part.transform.localPosition;
        var finalRotation = Quaternion.LookRotation(direction);
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 3;
            part.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        part.transform.localRotation = finalRotation;
    }

}
