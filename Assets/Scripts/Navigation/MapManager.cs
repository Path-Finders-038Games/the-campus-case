using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Navigation
{
    public class MapManager : MonoBehaviour
    {
        public GameObject[] Maps;
        
        private void Start()
        {
            if (Maps.Length == 0)
            {
                Debug.LogError("No maps found.");
                throw new NullReferenceException("No maps found.");
            }
            
            GameObject firstMap = Maps.First();
            
            firstMap.SetActive(true);
            
            foreach (GameObject map in Maps.Where(m => m != firstMap))
            {
                map.SetActive(false);
            }
        }

        public void SwitchMap(string mapName)
        {
            // Reset all maps
            foreach (GameObject map in Maps.Where(m => m.name != mapName)) map.SetActive(false);

            // Find the map by name and activate it
            GameObject selectedMap = Array.Find(Maps, m => m.name == mapName);

            if (selectedMap == null)
            {
                Debug.LogError($"Map not found: {mapName}");
                throw new NullReferenceException($"Map not found: {mapName}");
            }

            selectedMap.SetActive(true);
        }
    }
}