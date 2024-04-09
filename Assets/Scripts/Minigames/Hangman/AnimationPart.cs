using System.Collections;
using UnityEngine;

//class containing the part object and information on positions for the animation
public class AnimationTransform : MonoBehaviour
{
    // the part
    public GameObject Part;

    //the transform component from the part
    public Transform PartTansform;

    //vector 3 objects containing the relative positon and rotation to the parent 
    private Vector3 _assembled_pos;
    private Vector3 _assembled_rot;

    //vector 3 objects containing the relative target position and rotation for the part to have relative to the parent in the unassembled state
    private Vector3 _dissasembled_pos;
    private Vector3 _dissasembled_rot;

    //bool to keep track of assembly progress
    public bool Completed;

    // constructor for the class
    // rotation is done in EulerAngles to use Vector3 objects
    public AnimationTransform(GameObject Part, Vector3 transform_dissassembled_position, Vector3 transform_dissassembled_rotation)
    {
        this.Part = Part;
        PartTansform = this.Part.transform;

        _assembled_pos = this.Part.transform.localPosition;
        _assembled_rot = this.Part.transform.localEulerAngles;

        _dissasembled_pos = transform_dissassembled_position;
        _dissasembled_rot = transform_dissassembled_rotation;

        Completed = false;
    }

    // method to set the position of the part in dissasembled state
    public IEnumerator SetupPos()
    {

        Vector3 currentPos = PartTansform.localPosition;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current position and target position
            PartTansform.localPosition = Vector3.Lerp(currentPos, _dissasembled_pos, t);
            yield return null;
        }
        //explicit assignment for precision
        PartTansform.localPosition = _dissasembled_pos;
    }

    //method to set the rotation of the part in the dissassmebled state
    public IEnumerator RotatesSetup()
    {
        Vector3 startRotation = PartTansform.localEulerAngles;
        Vector3 finalRotation = _dissasembled_rot;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current rotation and target rotation
            PartTansform.localEulerAngles = Vector3.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        //explicit assignment for precision
        PartTansform.localEulerAngles = finalRotation;
    }

    // method to set the position of the part in asembled state
    public IEnumerator MoveToPosition()
    {
        Vector3 currentPos = PartTansform.localPosition;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current position and target position
            PartTansform.localPosition = Vector3.Lerp(currentPos, _assembled_pos, t);
            yield return null;
        }
        //explicit assignment for precision
        PartTansform.localPosition = _assembled_pos;
    }

    //method to set the rotation of the part in the assmebled state
    public IEnumerator RotateToDirection()
    {
        Vector3 startRotation = PartTansform.localEulerAngles;
        Vector3 finalRotation = _assembled_rot;
        float t = 0f;
        while (t <= 1f)
        {
            // total time for the animation to complete
            t += Time.deltaTime / 1;
            //linear interpolation of the current rotation and target rotation
            PartTansform.localEulerAngles = Vector3.Lerp(startRotation, finalRotation, t);
            yield return null;
        }
        //explicit assignment for precision
        PartTansform.localEulerAngles = finalRotation;
    }

}
