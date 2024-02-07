using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string Text;
    public bool IsRead;
    public Dialogue(string text)
    {
        Text = text;
        IsRead = false;
    }
}
