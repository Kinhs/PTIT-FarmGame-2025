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

    private void Start()
    {
        LoadFromSaveManager();
    }

    public int woodAmount;
    public int stoneAmount;

    public void LoadFromSaveManager()
    {
        woodAmount = SaveManager.instance.Data.wood;
        stoneAmount = SaveManager.instance.Data.stone;
    }

    public void SaveToSaveManager()
    {
        SaveManager.instance.Data.wood = woodAmount;
        SaveManager.instance.Data.stone = stoneAmount;
    }
}
