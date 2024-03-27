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

        private string _currentMap;
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

            // Check if there is one touch and if the touch is the beginning
            // of the touch input (to prevent multiple touches at once and holding down).
            if (Input.touchCount != 1 || Input.touches[0].phase != TouchPhase.Began) return;

            // Create a list of ARRaycastHits to store the hit results. This is used to find the closest plane.
            // Call the Raycast method and store the hit results in the list. The TrackableType is set to Planes.
            // The function returns true if a plane is hit, and false if not. If the function returns false, return.
            // The function also returns false if the hitResults list is empty, so this is checked as well.
            List<ARRaycastHit> hitResults = new();
            if (!_raycastManager.Raycast(Input.touches[0].position, hitResults,
                    TrackableType.Planes)) return;
            
            _currentMap = DataManager.CurrentMap;
            
            // Spawn the prefab at the first hit position. Cast the ARRaycastHit to an ARRaycastHit to prevent null reference.
            SpawnPrefab(hitResults.First());
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
                    .Contains(_currentMap)) return Default;

            // Return the minigame prefab that matches the current map.
            return Minigames
                .Where(e => SceneLoader.GetMinigameMapName(e.MinigameName) == _currentMap)
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