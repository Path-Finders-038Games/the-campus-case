
//class of the bullet
using UnityEngine;

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
        transform.position = Vector3.MoveTowards(transform.position, _target, BulletSpeed * Time.deltaTime);

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