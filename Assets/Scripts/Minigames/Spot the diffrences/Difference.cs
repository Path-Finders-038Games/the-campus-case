using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//helper class featuring the 2 objects and a boolean for progress
public class Difference : MonoBehaviour
{
    //original object part
    public GameObject Original { get; set; }

    //different object part
    public GameObject Different { get; set; }

    //boolean keeping track of if it has been found
    public bool Completed {  get; set; }

    //consctructor
    public Difference(GameObject original, GameObject different)
    {
        Original = original;
        Different = different;
        Completed = false;
    }
}
