using UnityEngine;

namespace Minigames.PaperPlanes
{
    public class PaperPlanes : MonoBehaviour
    {
        private const int WinCondition = 10;
        private const int LoseCondition = 10;
        
        private void Start()
        {
            PaperPlanesData.ResetData();
        }
        
        private void Update()
        {
            if (PaperPlanesData.PlanesHit >= WinCondition)
            {
                Debug.Log("You win!");
            }
            else if (PaperPlanesData.PlanesMissed >= LoseCondition)
            {
                Debug.Log("You lose!");
            }
        }
    }
}