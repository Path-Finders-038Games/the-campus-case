using System;
using UnityEngine;

namespace Navigation.FreeRoam
{
    public class CameraMovement : MonoBehaviour
    {
        private const float MinZoom = 1f;
        private const float MaxZoom = 50f;

        private Camera _camera;
        private float _fingerDistance;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            // HandlePan();
            HandleZoom();

            RestrictCamera();
        }

        private void HandlePan()
        {
            // If there is no finger movement, then return
            if (Input.touchCount != 1 || Input.GetTouch(0).phase != TouchPhase.Moved) return;

            // Move the camera based on finger movement
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float newX = -touchDeltaPosition.x;
            float newY = touchDeltaPosition.y;

            // Move the camera
            transform.Translate(newY, 0, newX, Space.World);
        }

        private void HandleZoom()
        {
            if (Input.touchCount != 2) return;

            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                // Store the initial distance between two fingers and the middle point when pinch gesture begins.
                _fingerDistance = Vector2.Distance(touch0.position, touch1.position);
            }
            else
            {
                // get current distance between fingers.
                float currentFingerDistance = Vector2.Distance(touch0.position, touch1.position);

                // calculate the delta between the current distance and the initial distance.
                float delta = currentFingerDistance - _fingerDistance;

                // if the delta is greater than 0.1, then zoom in/out.
                // this is to prevent the camera from zooming in/out too quickly.
                // this value is randomly picked. It can be adjusted to your liking.
                if (Math.Abs(delta) > 0.1f)
                {
                    // calculate the zoom amount.
                    float zoomAmount = _camera.orthographicSize - delta * 0.01f;

                    // take into account the current zoom level.
                    // move faster when zoomed out and slower when zoomed in.
                    float zoomDelta = _camera.orthographicSize - MinZoom;
                    
                    Debug.Log($"delta: {zoomDelta}, zoomAmount: {zoomAmount}");
                    
                    // zoomAmount *= zoomDelta * 0.1f;

                    // set the new zoom level.
                    _camera.orthographicSize = zoomAmount;
                }

                // store the current finger distance for the next frame.
                _fingerDistance = currentFingerDistance;
            }
        }

        private void RestrictCamera()
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MinZoom, MaxZoom);
        }
    }
}