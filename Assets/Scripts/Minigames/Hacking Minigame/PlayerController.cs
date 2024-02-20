using UnityEngine;

namespace Minigames.Hacking_Minigame
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        private Vector2 _startPos;
        private bool hasMoved;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                _startPos = Input.GetTouch(0).position;
                hasMoved = false;

            }

            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) 
            {
                Vector2 currentPos = Input.GetTouch(0).position;
            
                Vector2 delataPos = _startPos- currentPos;
            

                if (!hasMoved) 
                {
                    switch (delataPos.x)
                    {
                        case > 100:
                            SwitchLaneLeft();
                            break;
                        case < -100:
                            SwitchLaneRight();
                            break;

                        default:
                            break;
                    }
                }

            }

        }

        private void MoveObject(float distance)
        {

            Vector3 currentPos = transform.position;
            hasMoved = true;
            if (currentPos.x == distance)
            {
                return;
            }

            Vector3 newPosition = new(currentPos.x + distance, currentPos.y, currentPos.z);

            transform.position = newPosition;
        }


        private void SwitchLaneRight()
        {
            hasMoved = true;
            if(2 != GameController.gameController.CurrentLane)
            {

                GameController.gameController.CurrentLane++;
                transform.position = new Vector3(GameController.gameController.Lanes[GameController.gameController.CurrentLane].transform.position.x, transform.position.y, transform.position.z);
           
            }
        }

        private void SwitchLaneLeft()
        {
            hasMoved = true;
            if (0 != GameController.gameController.CurrentLane)
            {
                GameController.gameController.CurrentLane--;
                transform.position = new Vector3(GameController.gameController.Lanes[GameController.gameController.CurrentLane].transform.position.x, transform.position.y, transform.position.z);
            
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
