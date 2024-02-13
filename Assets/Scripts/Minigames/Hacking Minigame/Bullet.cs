using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed;
        public float distance;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,distance,transform.position.z),bulletSpeed * Time.deltaTime); 
            if(transform.position.y >= distance)
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
