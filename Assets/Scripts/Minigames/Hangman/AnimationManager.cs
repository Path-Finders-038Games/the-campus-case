using Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Hangman Hangman_Game;
    private int progress = 0;

    private List<AnimationTransform> Parts;

    public GameObject Plank_Middle;
    public GameObject Plank_Upper;
    public GameObject Support_Up;
    public GameObject Rope;
    public GameObject Rope2;
    public GameObject Seat;
    public GameObject Bear_head;
    public GameObject Bear_Body;
    public GameObject Bear_Arm_Right;
    public GameObject Bear_Arm_Left;
    public GameObject Bear_Leg_Left;
    public GameObject Bear_Leg_Right;

    public void Setup()
    {
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
