using UnityEngine;

namespace Art._3D.Minigame_Assets.Mastermind.Scripts
{
    public class Draggable3DObject : MonoBehaviour
    {
        private bool isDragging = false;
        private Vector3 offset;

        private void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPos();
        }

        private void OnMouseUp()
        {
            isDragging = false;
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector3 mousePos = GetMouseWorldPos();
                transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, mousePos.z + offset.z);
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}
