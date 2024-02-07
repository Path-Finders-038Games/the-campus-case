using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Numerics;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class SpawnPrefab : MonoBehaviour
{

    private GameObject _spanwNew;
    ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject Prefab;
    private ARRaycastHit _planeHit;
    private float _distance = 999;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            _distance = 999;
            if (raycastManager.Raycast(Input.touches[0].position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                foreach (var plane in hits)
                {
                    if (plane.distance < _distance)
                    {
                        _distance = plane.distance;
                        _planeHit = plane;
                    }
                }

                if (_spanwNew == null)
                {
                    UnityEngine.Quaternion temp = _planeHit.pose.rotation;
                    temp.eulerAngles = new UnityEngine.Vector3(0,Camera.main.transform.rotation.eulerAngles.y,0);
                    _spanwNew = Instantiate(Prefab, _planeHit.pose.position, temp);
                }
            }

        }
    }

    public void Destroy()
    {
        Destroy(_spanwNew);
    }
}

