using UnityEngine;
using System.Collections.Generic;

public class ForestInitializer : MonoBehaviour
{
    private void Start()
    {
        if (MaterialInfo.instance == null || TimeController.instance == null)
            return;

        WoodTree[] trees = FindObjectsByType<WoodTree>(FindObjectsSortMode.None);
        List<string> treeIds = new List<string>();

        foreach (var tree in trees)
        {
            string id = GetTreeId(tree);
            if (!string.IsNullOrEmpty(id))
                treeIds.Add(id);
        }

        MaterialInfo.instance.InitializeForestForDay(
            TimeController.instance.currentDay,
            treeIds
        );

        foreach (var tree in trees)
        {
            tree.ApplyStateFromMaterialInfo();
        }
    }

    private string GetTreeId(WoodTree tree)
    {
        var field = typeof(WoodTree).GetField(
            "treeId",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance
        );

        return field.GetValue(tree) as string;
    }
}
