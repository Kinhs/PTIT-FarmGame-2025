using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{

    public static CropController instance;

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

    public enum CropType
    {
        pumpkin,
        lettuce,
        carrot,
        hay,
        potato,
        strawberry,
        tomato,
        avocado
    }

    public List<CropInfo> cropList = new List<CropInfo>();

    public CropInfo GetCropInfo(CropType cropToGet)
    {
        int position = -1;

        for (int i = 0; i < cropList.Count; i++)
        {
            if (cropList[i].cropType == cropToGet)
            {
                position = i;
            }
        }

        if (position >= 0)
        {
            return cropList[position];
        } else
        {
            return null;
        }
    }

    public void UseSeed(CropType seedToUse)
    {
        foreach (CropInfo info in cropList)
        {
            if(info.cropType == seedToUse )
            {
                info.seedAmount--;
            }
        }
    }

    public void AddCrop(CropType cropToAdd)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToAdd)
            {
                info.cropAmount++;
            }
        }
    }

    public void AddSeed(CropType seedToAdd, int amount)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == seedToAdd)
            {
                info.seedAmount += amount;
            }
        }
    }

    public void RemoveCrop(CropType cropToRemove)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToRemove)
            {
                info.cropAmount = 0;
            }
        }
    }

    public void SaveToSaveManager()
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == CropType.pumpkin)
            {
                SaveManager.instance.Data.pumpkinCrop = info.cropAmount;
                SaveManager.instance.Data.pumpkinSeed = info.seedAmount;
            }

            if (info.cropType == CropType.lettuce)
            {
                SaveManager.instance.Data.lettuceCrop = info.cropAmount;
                SaveManager.instance.Data.lettuceSeed = info.seedAmount;
            }

            if (info.cropType == CropType.carrot)
            {
                SaveManager.instance.Data.carrotCrop = info.cropAmount;
                SaveManager.instance.Data.carrotSeed = info.seedAmount;
            }

            if (info.cropType == CropType.hay)
            {
                SaveManager.instance.Data.hayCrop = info.cropAmount;
                SaveManager.instance.Data.haySeed = info.seedAmount;
            }

            if (info.cropType == CropType.potato)
            {
                SaveManager.instance.Data.potatoCrop = info.cropAmount;
                SaveManager.instance.Data.potatoSeed = info.seedAmount;
            }

            if (info.cropType == CropType.strawberry)
            {
                SaveManager.instance.Data.strawberryCrop = info.cropAmount;
                SaveManager.instance.Data.strawberrySeed = info.seedAmount;
            }

            if (info.cropType == CropType.tomato)
            {
                SaveManager.instance.Data.tomatoCrop = info.cropAmount;
                SaveManager.instance.Data.tomatoSeed = info.seedAmount;
            }

            if (info.cropType == CropType.avocado)
            {
                SaveManager.instance.Data.avocadoCrop = info.cropAmount;
                SaveManager.instance.Data.avocadoSeed = info.seedAmount;
            }

        }
    }

    public void LoadFromSaveManager()
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == CropType.pumpkin)
            {
                info.cropAmount = SaveManager.instance.Data.pumpkinCrop;
                info.seedAmount = SaveManager.instance.Data.pumpkinSeed;
            }

            if (info.cropType == CropType.lettuce)
            {
                info.cropAmount = SaveManager.instance.Data.lettuceCrop;
                info.seedAmount = SaveManager.instance.Data.lettuceSeed;
            }

            if (info.cropType == CropType.carrot)
            {
                info.cropAmount = SaveManager.instance.Data.carrotCrop;
                info.seedAmount = SaveManager.instance.Data.carrotSeed;
            }

            if (info.cropType == CropType.hay)
            {
                info.cropAmount = SaveManager.instance.Data.hayCrop;
                info.seedAmount = SaveManager.instance.Data.haySeed;
            }

            if (info.cropType == CropType.potato)
            {
                info.cropAmount = SaveManager.instance.Data.potatoCrop;
                info.seedAmount = SaveManager.instance.Data.potatoSeed;
            }

            if (info.cropType == CropType.strawberry)
            {
                info.cropAmount = SaveManager.instance.Data.strawberryCrop;
                info.seedAmount = SaveManager.instance.Data.strawberrySeed;
            }

            if (info.cropType == CropType.tomato)
            {
                info.cropAmount = SaveManager.instance.Data.tomatoCrop;
                info.seedAmount = SaveManager.instance.Data.tomatoSeed;
            }

            if (info.cropType == CropType.avocado)
            {
                info.cropAmount = SaveManager.instance.Data.avocadoCrop;
                info.seedAmount = SaveManager.instance.Data.avocadoSeed;
            }


        }
    }
}

[System.Serializable]
public class CropInfo
{
    public CropController.CropType cropType;
    public Sprite finalCrop, seedType, planted, growStage1, growStage2, ripe;

    public int seedAmount, cropAmount;

    [Range(0f, 100f)]
    public float growthFailChance;

    public float seedPrice, cropPrice;
}