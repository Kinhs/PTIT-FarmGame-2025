using UnityEngine;
using System.Collections.Generic;

public class MaterialInfo : MonoBehaviour
{
    public static MaterialInfo instance;

    [Range(0f, 1f)]
    public float choppedChance = 0.3f;

    [Range(0f, 1f)]
    public float oreDepletedChance = 0.25f;

    private Dictionary<string, bool> treeStates = new Dictionary<string, bool>();
    private Dictionary<string, bool> oreStates = new Dictionary<string, bool>();

    private int lastTreeInitDay = -1;
    private int lastOreInitDay = -1;

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

    /* ================= TREE ================= */

    public void RegisterTree(string treeId)
    {
        if (!treeStates.ContainsKey(treeId))
            treeStates.Add(treeId, false);
    }

    public bool IsTreeChopped(string treeId)
    {
        if (!treeStates.ContainsKey(treeId))
            treeStates[treeId] = false;

        return treeStates[treeId];
    }

    public void SetTreeChopped(string treeId, bool chopped)
    {
        treeStates[treeId] = chopped;
    }

    public void InitializeTreesForDay(int currentDay, IEnumerable<string> treeIds)
    {
        if (treeIds == null)
            return;

        if (currentDay == lastTreeInitDay)
            return;

        lastTreeInitDay = currentDay;

        foreach (var id in treeIds)
            treeStates[id] = Random.value < choppedChance;
    }

    /* ================= ORE ================= */

    public void RegisterOre(string oreId)
    {
        if (!oreStates.ContainsKey(oreId))
            oreStates.Add(oreId, false);
    }

    public bool IsOreDepleted(string oreId)
    {
        if (!oreStates.ContainsKey(oreId))
            oreStates[oreId] = false;

        return oreStates[oreId];
    }

    public void SetOreDepleted(string oreId, bool depleted)
    {
        oreStates[oreId] = depleted;
    }

    public void InitializeOresForDay(int currentDay, IEnumerable<string> oreIds)
    {
        if (oreIds == null)
            return;

        if (currentDay == lastOreInitDay)
            return;

        lastOreInitDay = currentDay;

        foreach (var id in oreIds)
            oreStates[id] = Random.value < oreDepletedChance;
    }
}
