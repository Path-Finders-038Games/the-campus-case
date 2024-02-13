using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TestPlaneLocker : MonoBehaviour
{
    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    public GameObject PositionLocker;
    bool planeFound;
    GameObject FloorPlane;
    static List<ARRaycastHit> hits = new();

    public
    // Start is called before the first frame update
    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (planeFound && FloorPlane != null)
        {
            PositionLocker.transform.position =
                   new Vector3(PositionLocker.transform.position.x, FloorPlane.transform.position.y, PositionLocker.transform.position.z);
        }
        else if (raycastManager.Raycast(transform.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            FloorPlane = hits[0].trackable.gameObject;
            planeFound = true;
        }
    }
}
