using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;
using Unity.VisualScripting;

public class MinigameLoader : MonoBehaviour
{
    [System.Serializable]
    public class Minigame
    {
        public string MapName;
        public GameObject minigame;
    }
    private GameObject _spanwNew;

    public Minigame[] Minigames;
    public GameObject Default;

    ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastHit _planeHit;
    private float _distance = 999;
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
                    SpawnPrefab();
                }
            }
        }

    }

    void SpawnPrefab()
    {
        UnityEngine.Quaternion temp = _planeHit.pose.rotation;
        //To have the placeble objects in the right rotation -90 is used
        temp.eulerAngles = new UnityEngine.Vector3(GetCorrectPrefab().transform.eulerAngles.x,temp.eulerAngles.y,temp.eulerAngles.z);
        _spanwNew = Instantiate(GetCorrectPrefab(), _planeHit.pose.position, temp);
    }

    GameObject GetCorrectPrefab()
    {
        if (!Minigames.Select(e => e.MapName).Contains(DataManager.Instance.CurrentMap))
        {
            return Default;
        }

        return Minigames.Where(e => e.MapName == DataManager.Instance.CurrentMap).Select(e => e.minigame).First();
    }

    public void DestroyPrefab()
    {
        Destroy(_spanwNew);
    }
}
