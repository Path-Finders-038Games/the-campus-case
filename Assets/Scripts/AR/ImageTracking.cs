using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ImageTracking : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _placeblePrefab;
        Dictionary<string, GameObject> spawnedPrefabs = new();
        private ARTrackedImageManager _trackedImageManager;

        private void Awake()
        {
            _trackedImageManager = FindAnyObjectByType<ARTrackedImageManager>();
            foreach (GameObject prefab in _placeblePrefab)
            {
                GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                newPrefab.name = prefab.name;
                spawnedPrefabs.Add(prefab.name, newPrefab);
                newPrefab.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _trackedImageManager.trackedImagesChanged += ImageChanged;
        }

        private void OnDisable()
        {
            _trackedImageManager.trackedImagesChanged += ImageChanged;
        }

        private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (ARTrackedImage trackedImage in eventArgs.added)
            {
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    UpdateImage(trackedImage);
                }
            }
            foreach (ARTrackedImage trackedImage in eventArgs.updated)
            {
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    UpdateImage(trackedImage);
                }
            }
        }

        private void UpdateImage(ARTrackedImage trackedImage)
        {
            string name = trackedImage.referenceImage.name;
            Vector3 position = trackedImage.transform.position;
            Vector3 rortation = trackedImage.transform.eulerAngles;

            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = position;
            prefab.transform.eulerAngles = rortation;
            prefab.SetActive(true);
        }
    }
}
