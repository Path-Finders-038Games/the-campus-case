using UnityEngine;

public class DebugPlayerPrefs : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
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
