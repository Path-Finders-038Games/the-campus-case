using UnityEngine;

namespace Art.Map.Lines
{
    public class LineValueScript : MonoBehaviour
    {
        MeshRenderer meshRend;
        public float speed;
        float filledAmount;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (filledAmount < 2)
            {
                filledAmount += speed * Time.deltaTime;
            }
            meshRend = this.GetComponent<MeshRenderer>();
            meshRend.material.SetFloat("_fillAmount", filledAmount);
        }
    }
}
