using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        private Vector2 _startPos;
        private bool _hasMoved;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                _startPos = Input.GetTouch(0).position;
                _hasMoved = false;

            }

            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) 
            {
                Vector2 _currentPos = Input.GetTouch(0).position;
            
                Vector2 _deltaPos = _startPos- _currentPos;
            

                if (!_hasMoved) 
                {
                    switch (_deltaPos.x)
                    {
                        case > 100:
                            SwitchLane(true);
                            break;
                        case < -100:
                           SwitchLane(false);
                            break;

                        default:
                            break;
                    }
                }

            }

        }

        private void SwitchLane(bool directionLeft)
        {
            _hasMoved = true;
            if(GameController.gameController.CurrentLane > 0&& GameController.gameController.CurrentLane < 2)
            {
                if(directionLeft)
                {
                    GameController.gameController.CurrentLane--;
                    transform.position = new Vector3(GameController.gameController.Lanes[GameController.gameController.CurrentLane].transform.position.x, transform.position.y, transform.position.z);
                }
                else
                {
                    GameController.gameController.CurrentLane++;
                    transform.position = new Vector3(GameController.gameController.Lanes[GameController.gameController.CurrentLane].transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }

       

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            GameController.gameController.Health--;
            if (GameController.gameController.Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
