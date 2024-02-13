using UnityEngine;

namespace Art.Camera
{
    public class TestCam : MonoBehaviour
    {
        public Animator animator;
        public void TakePicture()
        {
            animator.SetTrigger("EquipState");
        }
    }
}
