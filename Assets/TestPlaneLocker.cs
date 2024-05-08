using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TestPlaneLocker : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    public GameObject PositionLocker;
    private bool planeFound;
    private GameObject FloorPlane;
    private static List<ARRaycastHit> hits = new();

    public void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();

    }

    private void Update()
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
