using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Art.Map
{
    public class MapTesting : MonoBehaviour
    {
        public Animator animator;
        bool mapOpen;

        void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "MapScroll")
                    {
                        EnterMap();   
                    }
                }
            }
        }
        public void Togglemap()
        {
            animator.SetTrigger("ToggleMap");
            if (mapOpen)
            {
                mapOpen = false;
            }
            else
            {
                mapOpen = true;
                StartCoroutine(Mapdelay());
            }
        }

        public IEnumerator Mapdelay()
        {
            yield return new WaitForSeconds(2f);
            EnterMap();
        }
        public void ToggleDesk()
        {
            animator.SetTrigger("Despawn");
        }

        public void EnterMap()
        {
            SceneLoader.LoadScene(GameScene.Navigation);
        }

        public void ExitMap()
        {
            SceneLoader.LoadScene(GameScene.MainMenu);
        }
    }
}
