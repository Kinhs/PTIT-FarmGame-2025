using System.Collections.Generic;
using UnityEngine;

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

    //public CropInfo GetCropInfo(CropType cropToGet)
    //{
    //    int position = -1;

    //    for (int i = 0; i < cropList.Count; i++)
    //    {
    //        if (cropList[i].cropType == cropToGet)
    //        {
    //            position = i;
    //        }
    //    }

    //    if (position >= 0)
    //    {
    //        return cropList[position];
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //public void UseSeed(CropType seedToUse)
    //{
    //    foreach (CropInfo info in cropList)
    //    {
    //        if (info.cropType == seedToUse)
    //        {
    //            info.seedAmount--;
    //        }
    //    }
    //}

    //public void AddCrop(CropType cropToAdd)
    //{
    //    foreach (CropInfo info in cropList)
    //    {
    //        if (info.cropType == cropToAdd)
    //        {
    //            info.cropAmount++;
    //        }
    //    }
    //}

    //public void AddSeed(CropType seedToAdd, int amount)
    //{
    //    foreach (CropInfo info in cropList)
    //    {
    //        if (info.cropType == seedToAdd)
    //        {
    //            info.seedAmount += amount;
    //        }
    //    }
    //}

    //public void RemoveCrop(CropType cropToRemove)
    //{
    //    foreach (CropInfo info in cropList)
    //    {
    //        if (info.cropType == cropToRemove)
    //        {
    //            info.cropAmount = 0;
    //        }
    //    }
    //}
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