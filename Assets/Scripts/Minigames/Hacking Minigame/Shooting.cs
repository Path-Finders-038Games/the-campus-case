using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class Shooting : MonoBehaviour
    {
        private float _timer;
        public GameObject bullet;
        public float relodeTime;
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
    }
}
