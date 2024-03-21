using UnityEngine;

namespace Dialog
{
    public class Dialogue : ScriptableObject
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