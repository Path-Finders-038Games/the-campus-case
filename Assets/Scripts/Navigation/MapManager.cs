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
            foreach (var map in Maps)
            {
                map.SetActive(false);
            }

            GameObject selectedMap = Array.Find(Maps, map => map.name == nameMap);
            if (selectedMap != null)
            {
                selectedMap.SetActive(true);
            }else
            {
                Debug.Log("Map not found: " + nameMap);   
            }
        }

        public void NextMap()
        {
            foreach (var map in Maps)
            {
                map.SetActive(false);
            }
            if(Mapnnmber > 6)
            {
                Mapnnmber = 0;
            }

            Maps[Mapnnmber].SetActive(true);
            Mapnnmber++;

        }
    }
}
