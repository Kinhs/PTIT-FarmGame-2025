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