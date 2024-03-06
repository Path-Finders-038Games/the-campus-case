using System;
using UnityEngine;

namespace Navigation
{
    public class MapManager : MonoBehaviour
    {
        public GameObject[] Maps;
        public int Mapnnmber;

        public void SwitchMap(string nameMap)
        {
            // Reset all maps
            foreach (GameObject map in Maps) map.SetActive(false);
            
            // Find the map by name and activate it
            GameObject selectedMap = Array.Find(Maps, map => map.name == nameMap);
            
            if (selectedMap == null)
            {
                Debug.LogWarning("Map not found: " + nameMap);
                return;
            }
            
            selectedMap.SetActive(true);
        }

        public void NextMap()
        {
            // Reset all maps
            foreach (GameObject map in Maps) map.SetActive(false);
            
            // Loop around when the end of the array is reached
            if (Mapnnmber > Maps.Length - 1) Mapnnmber = 0;
            
            Maps[Mapnnmber].SetActive(true);
            Mapnnmber++;
        }
    }
}