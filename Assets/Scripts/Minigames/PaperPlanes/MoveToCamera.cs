using System;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class MoveToCamera : MonoBehaviour
    {
        private Camera _camera;

        /// <summary>
        /// Sets the camera to the main camera.
        /// </summary>
        /// <exception cref="ArgumentNullException">MainCamera is not set in the Unity Editor.</exception>
        private void Start()
        {
            _camera = Camera.main;

            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera), "MainCamera is not set in the Unity Editor.");
            }
        }
        
        /// <summary>
        /// Updates the position of the game object to the camera's position.
        /// </summary>
        private void Update()
        {
            gameObject.transform.position = _camera.transform.position;
        }
    }
}