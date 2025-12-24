using UnityEngine;
using System.Collections.Generic;

public class WoodPickupSpawner : MonoBehaviour
{
    public static WoodPickupSpawner instance;

    [SerializeField] private WoodPickupEffect woodPrefab;
    [SerializeField] private int poolSize = 30;

    [SerializeField] private float spawnRadius = 0.4f;
    [SerializeField] private float flyDuration = 0.6f;

    private Queue<WoodPickupEffect> pool = new Queue<WoodPickupEffect>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            var wood = Instantiate(woodPrefab, transform);
            wood.gameObject.SetActive(false);
            pool.Enqueue(wood);
        }
    }

    public void SpawnWood(Vector3 center, int amount)
    {
        if (PlayerController.instance == null)
            return;

        Vector3 target = PlayerController.instance.transform.position;

        for (int i = 0; i < amount; i++)
        {
            if (pool.Count == 0)
                return;

            var wood = pool.Dequeue();

            Vector2 offset = Random.insideUnitCircle.normalized *
                             Random.Range(spawnRadius * 0.5f, spawnRadius);

            Vector3 spawnPos = center + (Vector3)offset;

            wood.Play(spawnPos, target, flyDuration);
            pool.Enqueue(wood);
        }
    }
}
