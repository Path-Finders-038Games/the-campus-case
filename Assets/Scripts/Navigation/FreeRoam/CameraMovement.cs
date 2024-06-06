using System;
using UnityEngine;

namespace Navigation.FreeRoam
{
    public class CameraMovement : MonoBehaviour
    {
        private const float CameraY = 5f;
        private const float MinZoom = 1f;
        private const float MaxZoom = 10f;

        private Camera _camera;

        private float _zoomFingerDistance;
        private Vector2 _panFingerDistance;

        private void Start()
        {
            _camera = Camera.main;

            if (_camera == null)
            {
                throw new NullReferenceException("Camera is not found in the scene.");
            }

            // Load data from DataManager
            _camera.transform.position = DataManager.CameraPosition;
            _camera.orthographicSize = DataManager.CameraOrthographicSize;
        }

        private void Update()
        {
            HandlePan();
            HandleZoom();

            RestrictCamera();
        }

        private void HandlePan()
        {
            // check if there is only one finger
            if (Input.touchCount != 1) return;

            Touch touch = Input.GetTouch(0);

            // Save the initial finger position
            if (touch.phase == TouchPhase.Began)
            {
                _panFingerDistance = touch.position;
            }

            // Get current finger position
            Vector2 currentFingerDistance = touch.position;

            // Calculate the difference between the current finger position and the previous finger position,
            // keeping into account the current camera orthographic size as a zoom level. Higher zoom level means more
            // zoomed out, so the panning should be faster.

            // Get the delta between both x and y positions. Negative one is to invert the movement,
            // as the camera moves instead of the world.
            // X and Z are used to move the camera on the X and Z axis in 3D space. The Y axis is the 2D plane of interaction
            // on the screen.
            float deltaX = (currentFingerDistance.x - _panFingerDistance.x) * _camera.orthographicSize * 0.001f * -1;
            float deltaZ = (currentFingerDistance.y - _panFingerDistance.y) * _camera.orthographicSize * 0.001f * -1;

            // Move the camera based on the delta values
            // Check if the delta is more than 0.1 to prevent the camera from moving too quickly
            if (Math.Abs(deltaX) > 0.01f || Math.Abs(deltaZ) > 0.01f)
            {
                float currentX = transform.localPosition.x;
                float currentZ = transform.localPosition.z;

                transform.localPosition = new Vector3(
                    currentX + deltaX,
                    CameraY,
                    currentZ + deltaZ);

                // transform.Translate(deltaX, 0, deltaZ);
            }

            // Save the finger movement for next frame
            _panFingerDistance = currentFingerDistance;

            // Save data to DataManager
            DataManager.CameraPosition = transform.position;
        }

        private void HandleZoom()
        {
            if (Input.touchCount != 2) return;

            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                // Store the initial distance between two fingers and the middle point when pinch gesture begins.
                _zoomFingerDistance = Vector2.Distance(touch0.position, touch1.position);
            }
            else
            {
                // get current distance between fingers.
                float currentFingerDistance = Vector2.Distance(touch0.position, touch1.position);

                // calculate the delta between the current distance and the initial distance.
                float delta = currentFingerDistance - _zoomFingerDistance;

                // if the delta is greater than 0.1, then zoom in/out.
                // this is to prevent the camera from zooming in/out too quickly.
                // this value is randomly picked. It can be adjusted to your liking.
                if (Math.Abs(delta) > 0.1f)
                {
                    // calculate the zoom amount.
                    float zoomAmount = _camera.orthographicSize - delta * 0.01f;

                    // set the new zoom level.
                    _camera.orthographicSize = zoomAmount;
                }

                // store the current finger distance for the next frame.
                _zoomFingerDistance = currentFingerDistance;

                // Save data to DataManager
                DataManager.CameraOrthographicSize = _camera.orthographicSize;
            }
        }

        private void RestrictCamera()
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MinZoom, MaxZoom);
            _camera.transform.position = new Vector3(
                Mathf.Clamp(_camera.transform.position.x, -10, 10),
                CameraY,
                Mathf.Clamp(_camera.transform.position.z, -10, 10));
        }
    }
}