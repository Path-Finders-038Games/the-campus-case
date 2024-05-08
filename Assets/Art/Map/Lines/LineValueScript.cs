using UnityEngine;

namespace Art.Map.Lines
{
    public class LineValueScript : MonoBehaviour
    {
        private MeshRenderer meshRend;
        public float speed;

        private float filledAmount;
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (filledAmount < 2)
            {
                filledAmount += speed * Time.deltaTime;
            }
            meshRend = GetComponent<MeshRenderer>();
            meshRend.material.SetFloat("_fillAmount", filledAmount);
        }
    }
}
