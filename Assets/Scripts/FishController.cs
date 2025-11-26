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