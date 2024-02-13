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
        public void Update()
        {
            // if our finger is on the screen and it has moved from its start position than do the code
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            
                // remembers the finger movement by storeing it in a vector 2
                Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // does the x and y calculation and moves the screen
                transform.Translate(TouchDeltaPosition.y * Speed, 0, -TouchDeltaPosition.x * Speed, Space.World);

            
                transform.localPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, MinX, MaxX),
                    Y,
                    Mathf.Clamp(transform.localPosition.z, MinZ, MaxZ));
            }

        }
    }
}
