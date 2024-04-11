using UnityEngine;
using UnityEngine.Localization.SmartFormat.Extensions;

namespace Minigames.Hacking_Minigame
{
    public class PlayerController : MonoBehaviour
    {
        // vector 2 position to keep track of where the users finger starts
        private Vector2 _startPos;

        // boolean to confirm if the user has moved yet
        private bool _hasMoved;

        // Update is called once per frame
        void Update()
        {
            //check if the users is touching the screen
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                //assign current location of the users finger to startpos and set moved to false
                _startPos = Input.GetTouch(0).position;
                _hasMoved = false;

            }

            //checks if the user has moved their finger
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) 
            {
                // creates a vector 2 based on the current position of the users finger
                Vector2 _currentPos = Input.GetTouch(0).position;
            
                //creates a differential position between the starting and current position of the users finger
                Vector2 _deltaPos = _startPos- _currentPos;

                //if the user hasnt moved yet, execute
                if (_hasMoved) return;
                //choose between outcomes based on the deltapos
                switch (_deltaPos.x)
                {
                    //if the users has moved to the left
                    case > 100:
                        SwitchLane(true);
                        break;

                    //if the user has moved to the right
                    case < -100:
                        SwitchLane(false);
                        break;

                    default:
                        break;
                }

            }

        }

        // method to change which lane the user is in
        private void SwitchLane(bool directionLeft)
        {
            //changes the boolean to signal the player has moved
            _hasMoved = true;

            //sets the currentlane to the left by 1
            if(directionLeft && GameController.gameController.CurrentLane > 0)
                {
                    GameController.gameController.CurrentLane--;
                }
            //sets the currentlane to the right by 1
            if (!directionLeft && GameController.gameController.CurrentLane < 2)
                {
                    GameController.gameController.CurrentLane++;
                }

           //assigns the player a position in the new currentlane
            transform.position = new Vector3(GameController.gameController.Lanes[GameController.gameController.CurrentLane].transform.position.x, transform.position.y, transform.position.z);
        }

       
        //if something touches the player destroy that object and reduce health by 1
        //if health reaches 0 destroy this gameobject
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

