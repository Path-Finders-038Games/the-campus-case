using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class MovementPlane : MonoBehaviour
    {
        // Editable in Unity Editor
        public float Speed = 1f;
        public float Rotate = 0.1f;

        private float _randomTime = 5;
        private bool _leftright;

        /// <summary>
        /// Sets the randomTime a random number between 1 and 3.
        /// Sets the direction of the plane to a random direction.
        /// </summary>
        private void Start()
        {
            _randomTime = Random.Range(1, 3);
            _leftright = Random.Range(0, 1) == 0;
        }

        /// <summary>
        /// Moves the plane forward and slightly downwards, as well as rotating it left and right.
        /// </summary>
        private void Update()
        {
            // Move plane forward and slightly downwards
            transform.Translate(Vector3.forward * (Time.deltaTime * Speed));
            transform.Translate(Vector3.down * (Time.deltaTime * 0.05f));

            // Decrease the random time by the time passed
            _randomTime -= Time.deltaTime;

            //Rotate the plane left and right
            if (_leftright)
            {
                //Rotate the plane down on the X axis of the rotation
                transform.Rotate(Vector3.down * Rotate);
            }
            else
            {
                //Rotate the plane up on the X axis of the rotation
                transform.Rotate(Vector3.up * Rotate);
            }
            
            // Return if it's not time to change direction
            if (!(_randomTime <= 0)) return;

            // Set a new random time for the plane to change direction
            _randomTime = Random.Range(1, 4) + Time.deltaTime;

            // Change the direction of the plane
            _leftright = !_leftright;
        }
    }
}