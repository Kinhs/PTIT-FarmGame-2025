using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public bool useSaveFile;
    public SaveData Data;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Data = useSaveFile ? SaveSystem.Load() : new SaveData();

            if (useSaveFile)
            {
                Debug.Log("Using save file at: " + Application.persistentDataPath);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        if (useSaveFile)
        {
            SaveSystem.Save(Data);
        }
    }
}
