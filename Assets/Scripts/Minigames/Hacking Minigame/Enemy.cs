using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Enemy : MonoBehaviour
    {
        public float Speed;
        public int Health;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -3, transform.position.z), Speed * Time.deltaTime);
            if(transform.position.y == -3)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Health--;
            Debug.Log("Enemy hit");
            if(Health == 0)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Health--;
            Debug.Log("Enemy hit");
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            Health--;
            Debug.Log("Enemy hit");
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag != "Enemy")
            {
                Health--;
                Debug.Log("Enemy hit");
                if (Health == 0)
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
