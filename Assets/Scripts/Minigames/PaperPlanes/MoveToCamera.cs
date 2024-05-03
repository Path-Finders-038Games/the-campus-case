using System;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class MoveToCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera), "MainCamera is not set in the Unity Editor.");
            }
        }
        
        private void Update()
        {
            gameObject.transform.position = _camera.transform.position;
        }
    }
}