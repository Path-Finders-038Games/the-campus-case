using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
            public bool UseAR;
        }

        public GameObject Default;
        public Minigame[] Minigames;

        public GameObject ExitArPlacementButton;
        public GameObject MovePrefabCloserButton;
        public GameObject MovePrefabFurtherButton;

        private GameObject _spawnedPrefab;
        private ARRaycastManager _raycastManager;
        private ARPlaneManager _planeManager;

        private void Start()
        {
            // Before loading anything for AR, check if UseAR is enabled for the current minigame.
            // Otherwise, just spawn the prefab for the minigame.
            if (Minigames.All(e => e.MinigameName != DataManager.SelectedMinigame) ||
                !Minigames.First(e => e.MinigameName == DataManager.SelectedMinigame).UseAR)
            {
                ExitArPlacementButton.SetActive(false);
                _planeManager.enabled = false;
                Instantiate(GetCorrectPrefab());
                return;
            }
            
            // Get the ARRaycastManager from the ARSessionOrigin.
            // This should be set in the Unity Editor, but it can't be since the prefab is not discoverable.
            _raycastManager = GetComponent<ARRaycastManager>();
            _planeManager = GetComponent<ARPlaneManager>();

            ExitArPlacementButton.SetActive(true);
            MovePrefabCloserButton.SetActive(false);
            MovePrefabFurtherButton.SetActive(false);
            _planeManager.enabled = true;
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

            // Spawn the prefab at the first hit position. Cast the ARRaycastHit to an ARRaycastHit to prevent null reference.
            SpawnPrefab(hitResults.First());
        }

        private void SpawnPrefab(ARRaycastHit plane)
        {
            // Get the rotation of the hit plane.
            Quaternion rotation = plane.pose.rotation;

            GameObject prefab = GetCorrectPrefab();

            // The prefab is rotated -90 degrees to have the correct rotation relative to the plane.
            rotation.eulerAngles = new Vector3(prefab.transform.eulerAngles.x, rotation.eulerAngles.y,
                rotation.eulerAngles.z);

            // Instantiate the prefab at the hit position with the correct rotation.
            _spawnedPrefab = Instantiate(prefab, plane.pose.position, rotation);

            ExitArPlacementButton.SetActive(false);
            MovePrefabCloserButton.SetActive(true);
            MovePrefabFurtherButton.SetActive(true);
            _planeManager.enabled = false;

            foreach (ARPlane arPlane in _planeManager.trackables)
            {
                arPlane.gameObject.SetActive(false);
            }
        }
    
        private GameObject GetCorrectPrefab()
        {
            // Check if the current map is not a minigame map.
            // If it's not, return the default prefab.
            if (Minigames.All(e => e.MinigameName != DataManager.SelectedMinigame)) return Default;

            // Return the minigame prefab that matches the current map.
            return Minigames
                .Where(e => e.MinigameName == DataManager.SelectedMinigame)
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
        
        public void MovePrefab(float distance)
        {
            // If the prefab is not spawned, return.
            if (_spawnedPrefab == null) return;

            // Move the prefab in the forward direction.
            // distance can be positive or negative to move the prefab forward or backward.
            _spawnedPrefab.transform.position += _spawnedPrefab.transform.up * distance;
        }
    }
}