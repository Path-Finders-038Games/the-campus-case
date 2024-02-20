using UnityEngine;

public class DebugPlayerPrefs : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    /// <summary>
    /// Clears all <see cref="PlayerPrefs"/>.
    /// </summary>
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

F