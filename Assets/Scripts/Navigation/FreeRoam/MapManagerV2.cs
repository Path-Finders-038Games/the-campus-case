using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Navigation.FreeRoam
{
    public class MapManagerV2 : MonoBehaviour
    {
        public List<MapModel> Maps;
        private GameObject _currentMap;
        private List<GameObject> _loadedMaps;

        private void Start()
        {
            _loadedMaps = new List<GameObject>();
            
            SwitchMap(MapName.T0);
        }

        public void SwitchMap(MapName mapName)
        {
            if (_currentMap != null)
            {
                _currentMap.SetActive(false);
            }

            if (_loadedMaps.Any(m => m.name.Contains(Converter.MapNameToString(mapName))))
            {
                GameObject map = _loadedMaps.Find(m => m.name.Contains(Converter.MapNameToString(mapName)));
                _currentMap = map;
            }
            else
            {
                MapModel mapModel = Maps.Find(m => m.Name == mapName);
                _currentMap = Instantiate(mapModel.MapPrefab, Vector3.zero, Quaternion.identity);
                _loadedMaps.Add(_currentMap);
            }
            
            _currentMap.SetActive(true);
            
            MapData.CurrentMap = mapName;
            DataManager.CurrentMapV2 = mapName;
        }
    }
}