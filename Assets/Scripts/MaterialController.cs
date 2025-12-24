using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public static MaterialController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int woodAmount;
    public int stoneAmount;
}
