using UnityEngine;

public class Case : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "CaseFileTop")
                {
                    OpenFile();
                }
            }
        }
    }

    public void OpenFile()
    {
        animator.SetTrigger("OpenFile");
    }


}
