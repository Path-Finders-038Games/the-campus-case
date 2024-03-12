using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

 
    public int sceneID;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneID);
    }
