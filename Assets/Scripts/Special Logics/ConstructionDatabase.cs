using System.Collections.Generic;
using UnityEngine;

public class ConstructionDatabase : MonoBehaviour
{
    public static ConstructionDatabase instance;

    public List<ConstructionData> constructions;

    private Dictionary<Construction.ConstructionType, ConstructionData> dataMap;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        dataMap = new Dictionary<Construction.ConstructionType, ConstructionData>();

        foreach (var data in constructions)
        {
            if (!dataMap.ContainsKey(data.type))
                dataMap.Add(data.type, data);
        }
    }

    public ConstructionData GetData(Construction.ConstructionType type)
    {
        return dataMap[type];
    }
}
