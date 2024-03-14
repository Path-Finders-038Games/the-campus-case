using Minigames.Hacking_Minigame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject Bullet_Prefab;
    public float ReloadTime;

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
            if (_timer > ReloadTime)
            {
                _timer = 0;
                GameObject Bullet = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            }
        }
    }

    public class Bullet : MonoBehaviour
    {
        public float BulletSpeed;
        public float Distance;

        private void Update()
        {
            BulletTravel();
        }

        public void BulletTravel()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Distance, transform.position.z), BulletSpeed * Time.deltaTime);
            if (transform.position.y >= Distance)
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
