using System;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class PlaneHit : MonoBehaviour
    {
        /// <summary>
        /// When the player hits the plane, increment the hit counter and destroy the plane.
        /// </summary>
        /// <param name="other">Default param. Object that this object collided with.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            PaperPlanesData.PlanesHit++;
            
            DestroyPlane();
        }

        /// <summary>
        /// When the plane leaves the boundary, increment the missed counter and destroy the plane.
        /// </summary>
        /// <param name="other">Default param. Object that this object collided with.</param>
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Boundary")) return;
            
            PaperPlanesData.PlanesMissed++;
            
            DestroyPlane();
        }

        /// <summary>
        /// Destroy this gameObject and set it to inactive.
        /// </summary>
        private void DestroyPlane()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}