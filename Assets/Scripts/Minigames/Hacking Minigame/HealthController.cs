using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Hacking_Minigame
{
    public class HealthController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Sprite[] HealthSprites;
        Image Image;
        private int count = 0;
        void Start()
        {
            Image = GetComponent<Image>();
            Image.sprite = HealthSprites[count];
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ReduceHealth ()
        {
            count++;
            Image.sprite = HealthSprites[count];
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            GameController.gameController.Health--;
            if (GameController.gameController.Health == 0)
            {
                Destroy(gameObject);
            }
        }

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