using System.Collections.Generic;
using System.Linq;
using Navigation;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Minigames
{
    [System.Serializable]
    public partial class Minigame
    {
        public string MapName;
        public GameObject MinigamePrefab;
    }

    public class MinigameLoader : MonoBehaviour
    {
        private GameObject _spawnNew;

        public Minigame[] Minigames;
        public GameObject Default;

        private ARRaycastManager _rayCastManager;
        private static readonly List<ARRaycastHit> Hits = new();
        private ARRaycastHit _planeHit;
        private float _distance = 999;

        private void Start()
        {
            _rayCastManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;

            _distance = 999;

            if (!_rayCastManager.Raycast(Input.touches[0].position, Hits,
                    UnityEngine.XR.ARSubsystems.TrackableType.Planes)) return;


            foreach (ARRaycastHit plane in Hits.Where(plane => plane.distance < _distance))
            {
                _distance = plane.distance;
                _planeHit = plane;
            }

            if (_spawnNew == null)
            {
                SpawnPrefab();
            }
        }

        private void SpawnPrefab()
        {
            Quaternion temp = _planeHit.pose.rotation;
            //To have the placeable objects in the right rotation -90 is used
            temp.eulerAngles = new Vector3(GetCorrectPrefab().transform.eulerAngles.x, temp.eulerAngles.y,
                temp.eulerAngles.z);
            _spawnNew = Instantiate(GetCorrectPrefab(), _planeHit.pose.position, temp);
        }

        private GameObject GetCorrectPrefab()
        {
            if (!Minigames.Select(e => e.MapName).Contains(DataManager.Instance.CurrentMap)) return Default;

            return Minigames.Where(e => e.MapName == DataManager.Instance.CurrentMap).Select(e => e.MinigamePrefab).First();
        }

        public void DestroyPrefab()
        {
            Destroy(_spawnNew);
        }
    }
}