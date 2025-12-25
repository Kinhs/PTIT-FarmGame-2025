using UnityEngine;
using System.Collections.Generic;

public class CaveInitializer : MonoBehaviour
{
    private void Start()
    {
        if (MaterialInfo.instance == null || TimeController.instance == null)
            return;

        Ore[] ores = FindObjectsByType<Ore>(FindObjectsSortMode.None);
        List<string> oreIds = new List<string>();

        foreach (var ore in ores)
        {
            var field = typeof(Ore).GetField(
                "oreId",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            );

            string id = field.GetValue(ore) as string;
            if (!string.IsNullOrEmpty(id))
                oreIds.Add(id);
        }

        MaterialInfo.instance.InitializeOresForDay(
            TimeController.instance.currentDay,
            oreIds
        );

        foreach (var ore in ores)
            ore.ApplyStateFromMaterialInfo();
    }
}
