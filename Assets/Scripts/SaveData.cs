using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("General")]
    public int day;
    public float money;

    [Header("Has Tools")]
    public bool hasFishingRod;
    public bool hasAxe;
    public bool hasPickaxe;

    [Header("Seeds")]
    public int pumpkinSeed;
    public int lettuceSeed;
    public int carrotSeed;
    public int haySeed;
    public int potatoSeed;
    public int strawberrySeed;
    public int tomatoSeed;
    public int avocadoSeed;

    [Header("Crops")]
    public int pumpkinCrop;
    public int lettuceCrop;
    public int carrotCrop;
    public int hayCrop;
    public int potatoCrop;
    public int strawberryCrop;
    public int tomatoCrop;
    public int avocadoCrop;

    [Header("Fish")]
    public int grayFish;
    public int greenFish;
    public int blueFish;
    public int orangeFish;
    public int redFish;

    [Header("Materials")]
    public int wood;
    public int stone;

    [Header("Constructions")]
    public bool builtWell;
    public bool builtWindmill;
    public bool builtGreenhouse;
    public bool builtHydroelectric;

    [Header("Grid")]
    public bool hasGrid;
    public List<InfoRow> grid;
}
