using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

   public int sceneID;
   /*public string sceneName;*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneID);
    }

    /*public void LoadSceneByString()
    {
        SceneManager.LoadScene(sceneName);
    }*/
}
