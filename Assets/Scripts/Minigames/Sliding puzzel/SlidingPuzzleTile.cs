using UnityEngine;

public class SlidingPuzzleTile : MonoBehaviour
{
    public int Id = 0;
    public Vector2Int Position = new Vector2Int();
    public string Name;
    // Start is called before the first frame update
    void Awake()
    {
        Name = gameObject.name;
    }
}
