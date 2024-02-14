using UnityEngine;
using UnityEngine.SceneManagement;

namespace Navigation
{
    public class NavigationController : MonoBehaviour
    {
        [System.Serializable]
        public class Step
        {
            [TextArea(0,5)]
            public string text_EN;
            [TextArea(0, 5)]
            public string text_NL;
            public Sprite image;
            public GameObject line;
            public bool hasMap;
            public GameObject map;
        }

        public Step[] Steps;
        public MapManager mapManager;
        public Stepbystep Stepbystep;
        public Camera camera;


        MeshRenderer meshRend;
        public float speed;
        float filledAmount;

        private void Start()
        { 
            UpdateGuide();
            mapManager.SwitchMap(DataManager.Instance.CurrentMap);
            meshRend = Steps[DataManager.Instance.CurrentStep].line.GetComponent<MeshRenderer>();
            filledAmount = 2;
        }

        private void Update()
        {
            if (meshRend == null)
            {
                return;
            }
            if (filledAmount < 2)
            {
                filledAmount += speed * Time.deltaTime;
            }
            meshRend.material.SetFloat("_fillAmount", filledAmount);
        }
        public void OnNext()
        {
            filledAmount = 0;
            if (meshRend != null)
            {
                meshRend.material.SetFloat("_fillAmount", filledAmount);
            }

            int currentstep = ++DataManager.Instance.CurrentStep;

            if (currentstep >= Steps.Length)
            {
                SceneManager.LoadScene(3);
            }

            if (Steps[currentstep].line != null)
            {
                meshRend = Steps[currentstep].line.GetComponent<MeshRenderer>();
            }

            if (Steps[currentstep].hasMap)
            {
                camera.transform.localPosition = new Vector3(0, 2.689579f, 0);
                mapManager.SwitchMap(Steps[currentstep].map.name);
                DataManager.Instance.CurrentMap = Steps[currentstep].map.name;
            }
            UpdateGuide();

            PlayerPrefs.SetInt("Currentstep",currentstep);
            PlayerPrefs.SetString("Currentmap",DataManager.Instance.CurrentMap);


        }

        private void UpdateGuide()
        {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "NL":
                    Stepbystep.UpdateGuide(Steps[DataManager.Instance.CurrentStep].text_NL, Steps[DataManager.Instance.CurrentStep].image);
                    break;


                default:
                    Stepbystep.UpdateGuide(Steps[DataManager.Instance.CurrentStep].text_EN, Steps[DataManager.Instance.CurrentStep].image);
                    break;
            }
        }


    }
}
