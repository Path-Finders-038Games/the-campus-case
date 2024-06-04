using UnityEngine;

namespace Navigation.FreeRoam
{
    public class MapSwitcher : MonoBehaviour
    {
        private MapManagerV2 _mm;
        public MapName MapName;

        private void Start()
        {
            _mm = FindObjectOfType<MapManagerV2>();
            
            if (_mm == null)
            {
                Debug.LogError("MapManager not found");
            }
        }

        public void SwitchMap()
        {
            _mm.SwitchMap(MapName);
        }
    }
}