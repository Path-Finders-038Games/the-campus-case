using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, -3, this.transform.position.z), speed * Time.deltaTime);
        if(this.transform.position.y == -3)
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
