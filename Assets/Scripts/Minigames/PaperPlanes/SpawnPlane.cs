using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class SpawnPlane : MonoBehaviour
    {
        //Declare the PaperPlane GameObject (This should also be done in the Unity Editor).
        public GameObject PaperPlane;
        private float _randomSpawn;

        private void Start()
        {
            //Set the randomSpawn a random number between 1 and 5, this will be the first plane to spawn.
            _randomSpawn = Time.time + 3f;
        }

        // Update is called once per frame
        private void Update()
        {
            //Spawn a plane after 4 seconds, then spawn a plane equal to the time elapsed + a random number between 1 and 5.
            if (Time.time < _randomSpawn) return;
            //Set the randomSpawn a random number between 1 and 5
            _randomSpawn = Random.Range(1, 5) + _randomSpawn;

            // If the game is not running, don't spawn any planes.
            // This is here and not higher up to still count up _randomSpawn, so the planes will spawn when the game starts.
            if (!PaperPlanesData.IsRunning) return;
            
            Vector3 position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            
            //Instantiate a new plane at the position of axysX and axysY (Between where the window is going to be).
            Instantiate(PaperPlane, position, gameObject.transform.rotation);
            
            PaperPlanesData.PlanesSpawned++;
        }
    }
}