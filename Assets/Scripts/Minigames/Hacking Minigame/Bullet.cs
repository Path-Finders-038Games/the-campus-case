using UnityEngine;


//class of the bullet
public class Bullet : MonoBehaviour
{
    //speed at which the bullet moves
    private float _bulletSpeed;

    public float Bulletspeed { get { return _bulletSpeed; } set { if (value > 0) _bulletSpeed = value; } }

    //maximum distance the bullet will travel
    private float _distance;

    public float Distance { get { return _distance; } set { if (value > 0) _distance = value; } }
    private Vector3 _target;

    private void Start()
    {
        _target = new Vector3(transform.position.x, _distance, transform.position.z);
    }
    private void Update()
    {
        BulletTravel();
    }

    //method to move the bullet
    public void BulletTravel()
    {
        //change the position of the bullet depending on the time in between frames to keep the speed consistent
        transform.position = Vector3.MoveTowards(transform.position, _target, _bulletSpeed * Time.deltaTime);

        //if the position of the bullet meets or exceeds the distance variable destroy the object
        if (transform.position.y >= _distance)
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