using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Hacking_Minigame
{
    public class HeathBar : MonoBehaviour
    {
        // Start is called before the first frame update
        public Sprite[] HealthSprites;
        Image Image;

        void Start()
        {
            Image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (GameController.gameController.Health)
            {
                case (0):
                    Image.sprite = HealthSprites[0];
                    break;
                case (1):
                    Image.sprite = HealthSprites[1];
                    break;
                case (2):
                    Image.sprite = HealthSprites[2];
                    break;
                case (3):
                    Image.sprite = HealthSprites[3];
                    break;
                default:
                    break;
            }
        }
    }
}
