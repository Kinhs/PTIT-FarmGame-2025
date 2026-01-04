using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData Data;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGame()
    {
        //Data = SaveSystem.Load();
    }

    public void SaveGame()
    {
        //SaveSystem.Save(Data);
    }
}
