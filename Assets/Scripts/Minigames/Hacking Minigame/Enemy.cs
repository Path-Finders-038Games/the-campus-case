using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Enemy : MonoBehaviour
    {
        //speed at which the enemy moves
        public float Speed;

        //health points the enemy has
        public int Health;

        //target position the enemy moves towards
        private Vector3 target;
        // Start is called before the first frame update
        void Start()
        {
            target = new Vector3(transform.position.x, -3, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            //moves towards the target position with a consistent speed
            transform.position = Vector3.MoveTowards(transform.position, target , Speed * Time.deltaTime);
            if(transform.position.y == -3)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnCollisionEnter(Collision collision)
        {
            Health--;
            Debug.Log("Enemy hit");
            if(Health == 0)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnTriggerEnter(Collider other)
        {
            Health--;
            Debug.Log("Enemy hit");
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
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
