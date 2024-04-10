using System;
using UnityEngine;

namespace Navigation
{
    public class PanAndClamp : MonoBehaviour
    {
        // the rate of progression with panning
        public float Speed = 1;
        public float MinX = -45f;
        public float MaxX = 30f;
        public float Y = 27.1f;
        public float MinZ = -18f;
        public float MaxZ = 26f;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private float GetZoomLevel()
        {
            float zoomLevel = 1;

            if (!_camera) return zoomLevel;

            zoomLevel = _camera.orthographicSize;

            return zoomLevel * 0.5f;
        }

        /// <summary>
        /// Moves the screen based on finger movement.
        /// ~~Fix the panning so that when the user is zoomed in or out, the panning moves the correct amount.~~
        /// Fixed enough for now.
        /// </summary>
        public void Update()
        {
            // If there is no finger movement, then return
            if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Moved) return;

            // Save the finger movement
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            float newX = -touchDeltaPosition.x * Speed * GetZoomLevel();
            float newY = touchDeltaPosition.y * Speed * GetZoomLevel();

            // Does the x and y calculation and moves the screen within the bounds
            transform.Translate(newY, 0, newX, Space.World);

            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, MinX, MaxX),
                Y,
                Mathf.Clamp(transform.localPosition.z, MinZ, MaxZ));
        }
    }
}