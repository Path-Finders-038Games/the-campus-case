using UnityEngine;

namespace Navigation
{
    public class MinimapZoom : MonoBehaviour
    {
        private Vector2 _initialMiddlePoint;
        private float _initialPinchDistance;
        private float _zoomSpeed = 0.003f;
        private float _minZoom = 0.7f;
        private float _maxZoom = 5f;

        /// <summary>
        /// Zooms in/out on the minimap when pinching with two fingers.
        /// </summary>
        void Update()
        {
            if (Input.touchCount != 2) return;

            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                // Store the initial distance between two fingers and the middle point when pinch gesture begins.
                _initialPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                _initialMiddlePoint = (touch0.position + touch1.position) * 0.5f;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                // Calculate the pinch amount based on the change in distance between two fingers.
                Vector2 middlePoint = (touch0.position + touch1.position) * 0.5f;
                float currentPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                float pinchAmount = (currentPinchDistance - _initialPinchDistance) * _zoomSpeed;

                // Adjust the camera's orthographic size to zoom in/out.
                Camera.main.orthographicSize -= pinchAmount;

                // Clamp the zoom level to be within the specified minZoom and maxZoom values.
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _minZoom, _maxZoom);

                // Calculate the offset between the initial middle point and the current middle point in world space.
                Vector2 offset = Camera.main.ScreenToWorldPoint(_initialMiddlePoint) - Camera.main.ScreenToWorldPoint(middlePoint);

                // Apply the offset to the camera position to keep the zoom centered around the middle point.
                Camera.main.transform.position += new Vector3(offset.x, offset.y, 0f);

                // Update the initial distance and middle point for the next frame.
                _initialPinchDistance = currentPinchDistance;
                _initialMiddlePoint = middlePoint;
            }
        }
    }
}
