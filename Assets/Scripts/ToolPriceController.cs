using UnityEngine;

public class ToolPriceController : MonoBehaviour
{
    public static ToolPriceController instance;

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

    public float axePrice;
    public float pickaxePrice;
}
