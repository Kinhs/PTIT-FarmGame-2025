using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridInfo : MonoBehaviour
{
    public static GridInfo instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadFromSaveManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool hasGrid;
    public List<InfoRow> theGrid;

    public void CreateGrid()
    {
        hasGrid = true;

        for (int y = 0; y < GridController.instance.blockRows.Count; y++)
        {
            theGrid.Add(new InfoRow());

            for (int x = 0; x < GridController.instance.blockRows[y].blocks.Count; x++)
            {
                theGrid[y].blocks.Add(new BlockInfo());
            }
        }
    }

    public void UpdateInfo(GrowBlock theBlock, int xPos, int yPos)
    {
        theGrid[yPos].blocks[xPos].currentStage = theBlock.currentStage;
        theGrid[yPos].blocks[xPos].isWatered = theBlock.isWatered;
        theGrid[yPos].blocks[xPos].cropType = theBlock.cropType;
        theGrid[yPos].blocks[xPos].growFailChance = theBlock.growFailChance;

    }

    public void GrowCrop()
    {
        for (int y = 0; y < theGrid.Count; y++)
        {
            for (int x = 0; x < theGrid[y].blocks.Count; x++)
            {
                if (theGrid[y].blocks[x].isWatered == true)
                {

                    float growFailTest = Random.Range(0f, 100f);

                    if (growFailTest > theGrid[y].blocks[x].growFailChance)
                    {
                        switch (theGrid[y].blocks[x].currentStage)
                        {
                            case GrowBlock.GrowthStage.planted:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.growing1;
                                break;

                            case GrowBlock.GrowthStage.growing1:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.growing2;
                                break;

                            case GrowBlock.GrowthStage.growing2:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.ripe;
                                break;
                        }
                    }

                    theGrid[y].blocks[x].isWatered = false;
                }

                if (theGrid[y].blocks[x].currentStage == GrowBlock.GrowthStage.ploughed)
                {
                    theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.barren;
                }
            }
        }
    }

    //private void Update()
    //{
    //    if (Keyboard.current.yKey.wasPressedThisFrame)
    //    {
    //        GrowCrop();
    //    }
    //}

    public void SaveToSaveManager()
    {
        SaveData data = SaveManager.instance.Data;

        data.hasGrid = hasGrid;

        if (!hasGrid)
        {
            data.grid = null;
            return;
        }

        data.grid = new List<InfoRow>();

        for (int y = 0; y < theGrid.Count; y++)
        {
            InfoRow newRow = new InfoRow();

            for (int x = 0; x < theGrid[y].blocks.Count; x++)
            {
                BlockInfo source = theGrid[y].blocks[x];

                BlockInfo newBlock = new BlockInfo
                {
                    isWatered = source.isWatered,
                    currentStage = source.currentStage,
                    cropType = source.cropType,
                    growFailChance = source.growFailChance
                };

                newRow.blocks.Add(newBlock);
            }

            data.grid.Add(newRow);
        }
    }

    public void LoadFromSaveManager()
    {
        SaveData data = SaveManager.instance.Data;

        hasGrid = data.hasGrid;

        if (!hasGrid || data.grid == null)
            return;

        theGrid = new List<InfoRow>();

        for (int y = 0; y < data.grid.Count; y++)
        {
            InfoRow newRow = new InfoRow();

            for (int x = 0; x < data.grid[y].blocks.Count; x++)
            {
                BlockInfo source = data.grid[y].blocks[x];

                BlockInfo newBlock = new BlockInfo
                {
                    isWatered = source.isWatered,
                    currentStage = source.currentStage,
                    cropType = source.cropType,
                    growFailChance = source.growFailChance
                };

                newRow.blocks.Add(newBlock);
            }

            theGrid.Add(newRow);
        }
    }

}


[System.Serializable]
public class BlockInfo
{
    public bool isWatered;
    public GrowBlock.GrowthStage currentStage;

    public CropController.CropType cropType;
    public float growFailChance;

}

[System.Serializable]
public class InfoRow
{
    public List<BlockInfo> blocks = new List<BlockInfo>();
}