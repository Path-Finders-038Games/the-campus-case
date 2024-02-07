using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCam : MonoBehaviour
{
    public Animator animator;
    public void TakePicture()
    {
        animator.SetTrigger("EquipState");
    }
}
