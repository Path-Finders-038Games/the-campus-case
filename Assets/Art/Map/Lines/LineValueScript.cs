using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineValueScript : MonoBehaviour
{
    MeshRenderer meshRend;
    public float speed;
    float filledAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (filledAmount < 2)
        {
            filledAmount += speed * Time.deltaTime;
        }
        meshRend = this.GetComponent<MeshRenderer>();
        meshRend.material.SetFloat("_fillAmount", filledAmount);
    }
}
