using System.Collections.Generic;
using UnityEngine;
using static CropController;

public class FishController : MonoBehaviour
{

    public static FishController instance;

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

    public enum FishType
    {
        gray,
        green,
        blue,
        orange,
        red
    }

    public List<FishInfo> fishList = new List<FishInfo>();

    public FishInfo GetFishInfo(FishType fishType)
    {
        int position = -1;

        for (int i = 0; i < fishList.Count; i++)
        {
            if (fishList[i].fishType == fishType)
            {
                position = i;
            }
        }

        if (position >= 0)
        {
            return fishList[position];
        }
        else
        {
            return null;
        }
    }

    public void AddFish(FishType fishToAdd)
    {
        foreach (FishInfo info in fishList)
        {
            if (info.fishType == fishToAdd)
            {
                info.amount++;
            }
        }
    }

    public void SellAllFishes()
    {
        foreach (FishInfo info in fishList)
        {
            if (info.amount > 0)
            {
                CurrencyController.instance.AddMoney(info.amount * info.price);
                info.amount = 0;
                AudioManager.instance.PlaySFXPitchAdjusted(6);
            }
        }
    }

    public void LoadFromSaveManager()
    {
        foreach (FishInfo info in fishList)
        {
            if (info.fishType == FishType.gray)
                info.amount = SaveManager.instance.Data.grayFish;

            if (info.fishType == FishType.green)
                info.amount = SaveManager.instance.Data.greenFish;

            if (info.fishType == FishType.blue)
                info.amount = SaveManager.instance.Data.blueFish;

            if (info.fishType == FishType.orange)
                info.amount = SaveManager.instance.Data.orangeFish;

            if (info.fishType == FishType.red)
                info.amount = SaveManager.instance.Data.redFish;

        }
    }

    public void SaveToSaveManager()
    {
        foreach (FishInfo info in fishList)
        {
            if (info.fishType == FishType.gray)
                SaveManager.instance.Data.grayFish = info.amount;

            if (info.fishType == FishType.green)
                SaveManager.instance.Data.greenFish = info.amount;

            if (info.fishType == FishType.blue)
                SaveManager.instance.Data.blueFish = info.amount;

            if (info.fishType == FishType.orange)
                SaveManager.instance.Data.orangeFish = info.amount;

            if (info.fishType == FishType.red)
                SaveManager.instance.Data.redFish = info.amount;

        }
    }
}

[System.Serializable]
public class FishInfo
{
    public FishController.FishType fishType;
    public Sprite sprite;

    public int amount;
    public float price;
    public float size;
    public float catchTime;

    [Range(0f, 100f)]
    public float commonPoint; // the lower, the rarer
}