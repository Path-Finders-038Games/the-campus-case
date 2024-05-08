using UnityEngine;

namespace Navigation
{
    public class PanAndClamp : MonoBehaviour
    {
        // the rate of progression with panning
        public float Speed;
        public float MinX = -45f;
        public float MaxX = 30f;
        public float Y = 27.1f;
        public float MinZ = -18f;
        public float MaxZ = 26f;
        
        /// <summary>
        /// Moves the screen based on finger movement.
        /// TODO: Fix the panning so that when the user is zoomed in or out, the panning moves the correct amount.
        /// </summary>
        public void Update()
        {
            // If there is no finger movement, then return
            if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Moved) return;

            // Save the finger movement
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // does the x and y calculation and moves the screen
            transform.Translate(touchDeltaPosition.y * Speed, 0, -touchDeltaPosition.x * Speed, Space.World);
            
            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, MinX, MaxX),
                Y,
                Mathf.Clamp(transform.localPosition.z, MinZ, MaxZ));
        }
    }
}