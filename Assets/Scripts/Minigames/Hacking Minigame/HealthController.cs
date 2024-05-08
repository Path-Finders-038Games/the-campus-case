using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Hacking_Minigame
{
    public class HealthController : MonoBehaviour
    {
        // array of all the stages of the healthbar
        public Sprite[] HealthSprites;

        //reference to the image component
        public Image Image;

        // Start is called before the first frame update
        private void Start()
        {
            //set the image sprite to the full HP sprite
            Image.sprite = HealthSprites[3];
        }

        // change the health sprite to a new one based on the health points left
        public void ChangeHealthSprite (int health)
        {
            Image.sprite = HealthSprites[health];
        }

        //if something touches this object reduce health by 1
        //if health reaches 0 destroy this gameobject
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            GameController.gameController.Health--;
            if (GameController.gameController.Health == 0)
            {
                Destroy(gameObject);
            }
        }

        //if something touches this object reduce health by 1
        //if health reaches 0 destroy this gameobject
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
            GameController.gameController.Health--;
            if (GameController.gameController.Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}