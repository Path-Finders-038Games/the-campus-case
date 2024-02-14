using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public int health;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -3, transform.position.z), speed * Time.deltaTime);
            if(transform.position.y == -3)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            health--;
            Debug.Log("Enemy hit");
            if(health == 0)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            health--;
            Debug.Log("Enemy hit");
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            health--;
            Debug.Log("Enemy hit");
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag != "Enemy")
            {
                health--;
                Debug.Log("Enemy hit");
                if (health == 0)
                {
                    Destroy(gameObject);
                }
            }
      
        }

        private void OnDestroy()
        {
            GameController.gameController.EnemyAlive--;
        }
    }
}
