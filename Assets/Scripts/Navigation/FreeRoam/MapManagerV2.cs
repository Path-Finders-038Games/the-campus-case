using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Navigation.FreeRoam
{
    public class MapManagerV2 : MonoBehaviour
    {
        private const float CameraY = 5f;

        public MapName InitialMap;
        public List<MapModel> Maps;
        private GameObject _currentMap;
        private MapName _currentMapName;
        private List<GameObject> _loadedMaps;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _loadedMaps = new List<GameObject>();

            SwitchMap(InitialMap);
        }

        private void Update()
        {
            if (_currentMapName != DataManager.CurrentMapV2)
            {
                SwitchMap(DataManager.CurrentMapV2);
            }
        }

        public void SwitchMap(MapName mapName)
        {
            if (mapName == MapName.None) return;
            
            MapName previousMapName = _currentMapName;

            if (Maps.All(m => m.Name != mapName))
            {
                Debug.LogError($"Map {mapName} not found in Maps list.");
                return;
            }

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
            _currentMapName = mapName;

            DataManager.CurrentMapV2 = mapName;

            var children = _currentMap.GetAllChildren();
            var buttonToPreviousMap = children.First(c => c.name.Contains(Converter.MapNameToString(previousMapName)));

            if (!buttonToPreviousMap)
            {
                _camera.transform.position = new Vector3(0, CameraY, 0);
                Debug.LogError($"Button to previous map ({previousMapName}) not found in {mapName}");
            }

            _camera.transform.position = new Vector3(
                buttonToPreviousMap.transform.position.x,
                CameraY,
                buttonToPreviousMap.transform.position.z
            );
        }
    }
}