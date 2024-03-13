using Minigames.Hacking_Minigame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject bullet;
    public float relodeTime;

    private float _timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameController.PlayGame)
        {
            _timer += Time.deltaTime;
            if (_timer > relodeTime)
            {
                _timer = 0;
                GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            }
        }
    }

    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed;
        public float distance;

        private void Update()
        {
            BulletTravel();
        }

        public void BulletTravel()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, distance, transform.position.z), bulletSpeed * Time.deltaTime);
            if (transform.position.y >= distance)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}
