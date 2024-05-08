using UnityEngine;

public class Case : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
    }

    /// <summary>
    /// Checks for a touch on the CaseFileTop and opens the file if it is touched.
    /// </summary>
    private void Update()
    {
        // If there are no touches or the touch is not a beginning touch, return.
        if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;
        // If the main camera is null, return.
        if (Camera.main == null) return;

        // Create a ray from the main camera to the touch position.
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        // If the raycast does not hit anything, return.
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        // If the object hit is not the CaseFileTop, return.
        if (hit.transform.gameObject.name != "CaseFileTop") return;
        
        OpenFile();
    }

    /// <summary>
    /// Opens the file by triggering the OpenFile animation.
    /// </summary>
    public void OpenFile()
    {
        animator.SetTrigger("OpenFile");
    }
}