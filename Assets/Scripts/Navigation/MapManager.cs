using System;
using System.Linq;
using UnityEngine;

namespace Navigation
{
    public class MapManager : MonoBehaviour
    {
        public GameObject[] Maps;
        
        private void Start()
        {
            if (Maps.Length < 1)
            {
                Debug.LogError("No maps found.");
                return;
            }

            if (!Maps.Select(m => m.name).Distinct().Contains(DataManager.CurrentMap))
            {
                DataManager.CurrentMap = Maps.First().name;
            }

            GameObject currentMap = Maps.First(m => m.name == DataManager.CurrentMap);
            
            if (currentMap == null)
            {
                Debug.LogError($"Map not found: {DataManager.CurrentMap}");
                throw new NullReferenceException($"Map not found: {DataManager.CurrentMap}");
            }
            
            currentMap.SetActive(true);
            
            foreach (GameObject map in Maps.Where(m => m != currentMap))
            {
                map.SetActive(false);
            }

            try
            {
                Camera.main.transform.position = DataManager.CameraPosition;
            }
            catch
            {
                Debug.LogWarning("Camera \"MainCamera\" not found.");
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
            
            DataManager.CurrentMap = mapName;
        }
    }
}