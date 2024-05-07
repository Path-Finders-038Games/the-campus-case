using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class MovementPlane : MonoBehaviour
    {
        //Set the speed and rotation of the plane (serialized so it can be changed in the inspector in Unity)
        private const float Speed = 1f;
        private const float Rotate = 0.1f;
        
        private float _randomTime = 5;
        private bool _leftright;

        // Start is called before the first frame update
        private void Start()
        {
            //Set the randomTime a random number between 1 and 3.
            _randomTime = Random.Range(1, 3);
            //Set a bool so the plane can rotate left (true) and right (false).
            _leftright = true;
        }

        // Update is called once per frame
        private void Update()
        {
            //Move the plane forward
            transform.Translate(Vector3.forward * (Time.deltaTime * Speed));
            
            // move the plane slightly downwards
            transform.Translate(Vector3.down * (Time.deltaTime * 0.05f));

            //set a timer: Pick a random number between 1 and 3 and subtract the time that has passed since the last frame.
            _randomTime -= Time.deltaTime;

            //Rotate the plane left and right
            if (_leftright)
            {
                //Rotate the plane down on the X axis of the rotation
                transform.Rotate(Vector3.down * Rotate);
                
                if (!(_randomTime <= 0)) return;
                
                //set a new timer
                _randomTime = Random.Range(1, 4) + Time.deltaTime;
            
                //set the bool to false so the plane can rotate right
                _leftright = false;
            }
            else
            {
                //Rotate the plane up on the X axis of the rotation
                transform.Rotate(Vector3.up * Rotate);
            
                //If the timer is done then do the following
                if (!(_randomTime <= 0)) return;
                
                //set a new timer
                _randomTime = Random.Range(1, 4) + Time.deltaTime;
            
                //set the bool to true so the plane can rotate left
                _leftright = true;
            }
        }


        // save the position and rotation of the GameObject in a struct
        public struct PositionRotation
        {
            public Vector3 position;
            public Quaternion rotation;
        }
    }
}