using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//helper class featuring the 2 objects and a boolean for progress
public class Difference : MonoBehaviour
{
    //original object part
    public GameObject Original;

    //different object part
    public GameObject Different;

    //boolean keeping track of if it has been found
    public bool Completed = false;
}
