using System;
using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class PlaneHit : MonoBehaviour
    {
        // Trigger the success event and destroy the plane when it hits the player
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log("Player hit plane!");
            
            PaperPlanesData.PlanesHit++;
            
            DestroyPlane();
        }

        // Destroy the plane when it exits the boundary
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Boundary")) return;
            
            Debug.Log("Plane left boundary");
            
            PaperPlanesData.PlanesMissed++;
            
            DestroyPlane();
        }

        private void DestroyPlane()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}