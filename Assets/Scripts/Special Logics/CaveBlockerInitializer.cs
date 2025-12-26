using UnityEngine;
using System.Collections.Generic;

public class CaveBlockerInitializer : MonoBehaviour
{
    private void Start()
    {
        if (MaterialInfo.instance == null || TimeController.instance == null)
            return;

        var blockers = Object.FindObjectsByType<CaveBlocker>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        List<string> blockerIds = new();

        foreach (var b in blockers)
            blockerIds.Add(b.BlockerId);

        MaterialInfo.instance.InitializeCaveBlockersForDay(
            TimeController.instance.currentDay,
            blockerIds
        );

        foreach (var b in blockers)
            b.ApplyState();
    }
}
