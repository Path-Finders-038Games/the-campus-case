using Minigames.Hacking_Minigame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    //object which will be shot
    public GameObject Bullet_Prefab;

    //speed at which the bullets are fired
    public float ReloadTime;

    // variable which keeps track of the time
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //only execute it the game is played
        if (GameController.gameController.PlayGame)
        {
            // add time that has passed to the time tracker variable
            _timer += Time.deltaTime;

            //if passed time exceeded the reloadtime, shoot a new bullet
            if (_timer > ReloadTime)
            {
                //reset the timer and instantiate a new bullet
                _timer = 0;
                GameObject Bullet = Instantiate(Bullet_Prefab, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            }
        }
    }


    //class of the bullet
    public class Bullet : MonoBehaviour
    {
        //speed at which the bullet moves
        public float BulletSpeed;

        //keep track of distance the bullet has traveled
        public float Distance;

        private Vector3 _target;

        private void Start()
        {
            _target = new Vector3(transform.position.x, Distance, transform.position.z);
        }
        private void Update()
        {

            BulletTravel();
        }
        
        //method to move the bullet
        public void BulletTravel()
        {
            //change the position of the bullet depending on the time in between frames to keep the speed consistent
            transform.position = Vector3.MoveTowards(transform.position, _target , BulletSpeed * Time.deltaTime);

            //if the position of the bullet meets or exceeds the distance variable destroy the object
            if (transform.position.y >= Distance)
            {
                Destroy(gameObject);
            }
        }

        // if the bullet touches anything, destroy the bullet
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);

        }

        // if the bullet touches anything, destroy the bullet
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }
    }
}
