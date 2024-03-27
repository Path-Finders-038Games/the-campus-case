using UnityEngine;
using UnityEngine.SceneManagement;

namespace Navigation
{
    public class NavigationController : MonoBehaviour
    {
        [System.Serializable]
        public class Step
        {
            [TextArea(0, 5)] public string text_EN;
            [TextArea(0, 5)] public string text_NL;
            public Sprite image;
            public GameObject line;
            public bool hasMap;
            public GameObject map;
        }

        public Step[] Steps;
        public MapManager mapManager;
        public Stepbystep Stepbystep;
        public Camera camera;

        private MeshRenderer meshRend;
        public float speed;
        private float filledAmount;

        /// <summary>
        /// Initializes the map and the guide.
        /// </summary>
        private void Start()
        {
            UpdateGuide();
            mapManager.SwitchMap(DataManager.CurrentMap);
            meshRend = Steps[DataManager.CurrentStep].line.GetComponent<MeshRenderer>();
            filledAmount = 2;
        }

        /// <summary>
        /// Updates the fill amount of the line.
        /// </summary>
        private void Update()
        {
            // If the mesh renderer is null, return.
            if (meshRend == null) return;

            // If the filled amount is less than 2, increase the filled amount.
            if (filledAmount < 2) filledAmount += speed * Time.deltaTime;

            // Set the fill amount of the line to the filled amount.
            meshRend.material.SetFloat("_fillAmount", filledAmount);
        }

        public void OnNext()
        {
            filledAmount = 0;

            // If the mesh renderer is not null, set the fill amount of the line to the filled amount.
            if (meshRend != null) meshRend.material.SetFloat("_fillAmount", filledAmount);

            // Increase the current step by 1.
            int currentstep = ++DataManager.CurrentStep;

            // If the current step is greater than or equal to the amount of steps, load the next scene.
            if (currentstep >= Steps.Length) SceneManager.LoadScene(3);

            // If the line is not null, set the mesh renderer to the line's mesh renderer.
            if (Steps[currentstep].line != null) meshRend = Steps[currentstep].line.GetComponent<MeshRenderer>();

            // If the current step has a map, set the camera's local position to the map's position and switch the map.
            if (Steps[currentstep].hasMap)
            {
                camera.transform.localPosition = new Vector3(0, 2.689579f, 0);
                mapManager.SwitchMap(Steps[currentstep].map.name);
                DataManager.CurrentMap = Steps[currentstep].map.name;
            }

            UpdateGuide();

            // Set the current step and map in the player preferences.
            PlayerPrefs.SetInt("Currentstep", currentstep);
            PlayerPrefs.SetString("Currentmap", DataManager.CurrentMap);
        }

        /// <summary>
        /// Updates the guide with the current step and language.
        /// </summary>
        private void UpdateGuide()
        {
            // Set the text to the current step's text based on the current language.
            string text = LanguageManager.GetLanguage() switch
            {
                LanguageManager.Language.Dutch => Steps[DataManager.CurrentStep].text_NL,
                LanguageManager.Language.English => Steps[DataManager.CurrentStep].text_EN,
                _ => Steps[DataManager.CurrentStep].text_EN
            };

            // Update the guide with the text and the current step's image.
            Stepbystep.UpdateGuide(text, Steps[DataManager.CurrentStep].image);
        }
    }
}