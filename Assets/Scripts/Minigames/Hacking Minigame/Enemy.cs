using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Enemy : MonoBehaviour
    {

        //speed at which the enemy moves
        private float _speed;

        public float Speed { get { return _speed; } set { if (value > 0) _speed = value; } }

        //health points the enemy has
        private int _health;

        public int Health { get { return _health; } set { if (value > 0) _health = value; } }
        //target position the enemy moves towards
        private Vector3 _target;

        // Start is called before the first frame update
        void Start()
        {
            _target = new Vector3(transform.position.x, -3, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            //moves towards the target position with a consistent speed
            transform.position = Vector3.MoveTowards(transform.position, _target , _speed * Time.deltaTime);
            if(transform.position.y == -3) Destroy(gameObject);
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnCollisionEnter(Collision collision)
        {
            _health--;
            Debug.Log("Enemy hit");
            if(_health == 0)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnTriggerEnter(Collider other)
        {
            _health--;
            Debug.Log("Enemy hit");
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _health--;
            Debug.Log("Enemy hit");
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }

        //reduce health if the enemy hits something, destroy the enemy if the health variable = 0
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag != "Enemy")
            {
                _health--;
                Debug.Log("Enemy hit");
                if (_health == 0)
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
