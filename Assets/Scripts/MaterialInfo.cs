using UnityEngine;
using System.Collections.Generic;

public class MaterialInfo : MonoBehaviour
{
    public static MaterialInfo instance;

    [Range(0f, 1f)]
    public float choppedChance = 0.3f;

    private Dictionary<string, bool> treeStates = new Dictionary<string, bool>();
    private int lastInitializedDay = -1;
    private bool forestInitialized = false;

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

    public void RegisterTree(string treeId)
    {
        if (!treeStates.ContainsKey(treeId))
            treeStates.Add(treeId, false);
    }

    public void InitializeForestForDay(int currentDay, IEnumerable<string> treeIds)
    {
        if (forestInitialized && currentDay == lastInitializedDay)
            return;

        forestInitialized = true;
        lastInitializedDay = currentDay;

        foreach (var id in treeIds)
        {
            bool chopped = Random.value < choppedChance;
            treeStates[id] = chopped;
        }
    }
}
