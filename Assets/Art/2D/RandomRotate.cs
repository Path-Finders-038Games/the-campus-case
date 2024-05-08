using UnityEngine;

namespace Art._2D
{
    public class RandomRotate : MonoBehaviour
    {
        public GameObject rotateObject;
        public float minRot, maxRot, steps;

        private Quaternion targetRot, startRot;
        // Start is called before the first frame update
        private void Start()
        {
            rotateObject = gameObject;
            startRot = rotateObject.transform.rotation;
            SetTargetRot();
        }

        // Update is called once per frame
        private void Update()
        {
            if (rotateObject.transform.rotation != targetRot)
            {
                rotateObject.transform.rotation = Quaternion.RotateTowards(rotateObject.transform.rotation, targetRot, steps * Time.deltaTime);
            }
            else
            {
                SetTargetRot();
            }
        }

        public void SetTargetRot()
        {
            targetRot = Quaternion.Euler(startRot.eulerAngles.x, startRot.eulerAngles.y, startRot.eulerAngles.z + Random.Range(minRot, maxRot));
        }
    }
}
