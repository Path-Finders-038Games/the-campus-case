using UnityEngine;

namespace Dialog
{
    public class Dialogue : MonoBehaviour
    {
        public string Text;
        public bool IsRead;
        public Dialogue(string text)
        {
            Text = text;
            IsRead = false;
        }
    }
}
