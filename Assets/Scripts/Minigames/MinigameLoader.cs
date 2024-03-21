using System.Collections.Generic;
using System.Linq;
using Navigation;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Minigames
{
    public class MinigameLoader : MonoBehaviour
    {
        /// <summary>
        /// This class is located here so it's visible in Unity.
        /// </summary>
        [System.Serializable]
        public class Minigame
        {
            public MinigameName MinigameName;
            public GameObject MinigamePrefab;
        }

        public GameObject Default;
        public Minigame[] Minigames;

        private GameObject _spawnedPrefab;

        private ARRaycastManager _raycastManager;
        private ARRaycastHit _planeHit;

        private static readonly List<ARRaycastHit> PreviousRaycastHits = new();

        private void Start()
        {
            // Get the ARRaycastManager from the ARSessionOrigin.
            // This should be set in the Unity Editor, but it can't be since the prefab is not discoverable.
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        private void Update()
        {
            // If the prefab is already spawned, return.
            if (_spawnedPrefab != null) return;

            // Check if there is more than 0 touches and if the touch is the beginning
            // of the touch (to prevent multiple touches at once and holding down).
            if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;

            // Set the distance to the maximum value. This is used to find the closest plane.
            // This value is decreased when a closer plane is found.
            float distance = float.MaxValue;

            // If the raycast does not hit any planes, return. Planes are the dotted surfaces
            // visible in the AR environment.
            if (!_raycastManager.Raycast(Input.touches[0].position, PreviousRaycastHits,
                    TrackableType.Planes)) return;

            // Find the closest plane. This is done by iterating through all the planes and
            // checking if the distance is smaller than the previous distance.
            foreach (ARRaycastHit plane in PreviousRaycastHits)
            {
                // If the current plane is not closer than the previous plane, exit the loop.
                if (!(plane.distance < distance)) continue;

                // Set the distance to the current plane's distance, and set the planeHit to the current plane.
                distance = plane.distance;
                _planeHit = plane;
            }

            // Spawn the prefab at the hit position.
            SpawnPrefab(_planeHit);
        }

        private void SpawnPrefab(ARRaycastHit plane)
        {
            // Get the rotation of the hit plane.
            Quaternion rotation = plane.pose.rotation;

            // The prefab is rotated -90 degrees to have the correct rotation relative to the plane.
            rotation.eulerAngles = new Vector3(GetCorrectPrefab().transform.eulerAngles.x, rotation.eulerAngles.y,
                rotation.eulerAngles.z);

            // Instantiate the prefab at the hit position with the correct rotation.
            _spawnedPrefab = Instantiate(GetCorrectPrefab(), plane.pose.position, rotation);
        }

        private GameObject GetCorrectPrefab()
        {
            // Check if the current map is not a minigame map.
            // If it's not, return the default prefab.
            if (!Minigames.Select(e => SceneLoader.GetMinigameMapName(e.MinigameName))
                    .Contains(DataManager.Instance.CurrentMap)) return Default;

            // Return the minigame prefab that matches the current map.
            return Minigames
                .Where(e => SceneLoader.GetMinigameMapName(e.MinigameName) == DataManager.Instance.CurrentMap)
                .Select(e => e.MinigamePrefab)
                .First();
        }

        public void DestroyPrefab()
        {
            // If the prefab is not spawned, return.
            if (_spawnedPrefab == null) return;

            // Destroy the prefab.
            Destroy(_spawnedPrefab);
        }
    }
}