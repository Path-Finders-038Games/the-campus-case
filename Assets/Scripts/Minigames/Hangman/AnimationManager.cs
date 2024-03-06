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

    private List<AnimationTransform> Parts;

    private GameObject Plank_Middle;
    private GameObject Plank_Upper;
    private GameObject Support_Up;
    private GameObject Rope;
    private GameObject Rope2;
    private GameObject Seat;
    private GameObject Bear_head;
    private GameObject Bear_Body;
    private GameObject Bear_Arm_Right;
    private GameObject Bear_Arm_Left;
    private GameObject Bear_Leg_Left;
    private GameObject Bear_Leg_Right;

    public void Setup()
    {
            foreach (Transform child in HangmanObject.transform)
            {
                    children.Add(child.gameObject);
            }

        Parts.Add(new AnimationTransform(Plank_Middle, Plank_Middle.transform));
        Parts.Add(new AnimationTransform(Plank_Upper, Plank_Upper.transform));
        Parts.Add(new AnimationTransform(Support_Up, Support_Up.transform));
        Parts.Add(new AnimationTransform(Rope, Rope.transform));
        Parts.Add(new AnimationTransform(Rope2, Rope2.transform));
        Parts.Add(new AnimationTransform(Seat, Seat.transform));
        Parts.Add(new AnimationTransform(Bear_head, Bear_head.transform));
        Parts.Add(new AnimationTransform(Bear_Body, Bear_Body.transform));
        Parts.Add(new AnimationTransform(Bear_Arm_Right, Bear_Arm_Right.transform));
        Parts.Add(new AnimationTransform(Bear_Arm_Left, Bear_Arm_Left.transform));
        Parts.Add(new AnimationTransform(Bear_Leg_Left, Bear_Leg_Left.transform));
        Parts.Add(new AnimationTransform(Bear_Leg_Right, Bear_Leg_Right.transform));
    }

    public void Animation_0()
    {

    }

    public void Animation_1()
    {

    }

    public void Animation_2()
    {

    }

    public void Animation_3()
    {

    }

    public void Animation_4()
    {

    }

    public void Animation_5()
    {

    }

    public void Animation_6()
    {

    }

    public void Animation_7()
    {

    }

    public void Animation_8()
    {

    }

    public void Animation_9()
    {

    }

    public void Animation_10()
    {

    }

    public void Animation_11()
    {

    }

    public void Animation_12()
    {

    }
    
    
}

public class AnimationTransform
{
    private GameObject part;
    private Transform assembled;
    private Transform dissasembled;
    private bool completed;

    public AnimationTransform(GameObject Part,Transform transform)
    {
        part = Part;
        assembled = transform;
        dissasembled = transform;
        completed = false;
    }
}
