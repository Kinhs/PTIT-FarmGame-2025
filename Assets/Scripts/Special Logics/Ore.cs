using UnityEngine;
using System.Collections;

public class Ore : MonoBehaviour
{
    [SerializeField] private OreType type;
    [SerializeField] private string oreId;

    [SerializeField] private int maxHits = 3;
    [SerializeField] private float shakeDuration = 0.12f;
    [SerializeField] private float shakeStrength = 0.08f;

    [SerializeField] private GameObject oreSprite;

    private int currentHits;
    private int rewardAmount;
    private Vector3 originalPosition;
    private bool isDepleted;

    private void Awake()
    {
        maxHits = Random.Range(2, 7);
        rewardAmount = maxHits / 2;

        currentHits = maxHits;
        originalPosition = transform.localPosition;

        MaterialInfo.instance.RegisterOre(oreId);
        ApplyStateFromMaterialInfo();
    }

    public void ApplyStateFromMaterialInfo()
    {
        bool depleted = MaterialInfo.instance.IsOreDepleted(oreId);

        if (depleted)
        {
            isDepleted = true;
            currentHits = 0;
            oreSprite.SetActive(false);
        }
        else
        {
            isDepleted = false;
            currentHits = maxHits;
            oreSprite.SetActive(true);
        }
    }

    public void TakeHit()
    {
        if (isDepleted)
            return;

        currentHits--;
        StartCoroutine(Shake());

        if (currentHits <= 0)
        {
            isDepleted = true;
            oreSprite.SetActive(false);

            MaterialInfo.instance.SetOreDepleted(oreId, true);

            ItemType itemType = type == OreType.stone
                ? ItemType.Stone
                : ItemType.Gold;

            ItemPickupSpawner.instance.Spawn(
                itemType,
                transform.position,
                rewardAmount
            );
        }
    }

    private IEnumerator Shake()
    {
        float time = 0f;

        while (time < shakeDuration)
        {
            Vector2 offset = Random.insideUnitCircle * shakeStrength;
            transform.localPosition = originalPosition + (Vector3)offset;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}

public enum OreType
{
    stone,
    gold
}
