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
            var field = typeof(WoodTree).GetField(
                "treeId",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            );

            string id = field.GetValue(tree) as string;
            if (!string.IsNullOrEmpty(id))
                treeIds.Add(id);
        }

        MaterialInfo.instance.InitializeTreesForDay(
            TimeController.instance.currentDay,
            treeIds
        );

        foreach (var tree in trees)
            tree.ApplyStateFromMaterialInfo();
    }
}
