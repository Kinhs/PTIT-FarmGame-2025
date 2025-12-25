using UnityEngine;
using System.Collections.Generic;

public class ItemPickupSpawner : MonoBehaviour
{
    public static ItemPickupSpawner instance;

    [System.Serializable]
    public class ItemPrefab
    {
        public ItemType type;
        public GameObject prefab;
    }

    [SerializeField] private List<ItemPrefab> itemPrefabs;
    [SerializeField] private float spawnRadius = 0.4f;
    [SerializeField] private float flyDuration = 0.5f;

    private Dictionary<ItemType, GameObject> prefabMap;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        prefabMap = new Dictionary<ItemType, GameObject>();
        foreach (var item in itemPrefabs)
            prefabMap[item.type] = item.prefab;
    }

    public void Spawn(ItemType type, Vector3 origin, int amount)
    {
        if (!prefabMap.ContainsKey(type))
            return;

        for (int i = 0; i < amount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = origin + (Vector3)offset;

            GameObject obj = Instantiate(prefabMap[type], spawnPos, Quaternion.identity);
            ItemPickup pickup = obj.GetComponent<ItemPickup>();

            pickup.FlyTo(
                PlayerController.instance.transform,
                flyDuration,
                () => OnPickupArrive(type)
            );
        }
    }

    private void OnPickupArrive(ItemType type)
    {
        switch (type)
        {
            case ItemType.Wood:
                MaterialController.instance.woodAmount += 1;
                break;

            case ItemType.Stone:
                MaterialController.instance.stoneAmount += 1;
                break;

            case ItemType.Gold:
                CurrencyController.instance.AddMoney(10);
                break;
        }
    }
}

public enum ItemType
{
    Wood,
    Stone,
    Gold
}
