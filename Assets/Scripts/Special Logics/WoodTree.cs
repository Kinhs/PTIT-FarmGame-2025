using UnityEngine;
using System.Collections;

public class WoodTree : MonoBehaviour
{
    [SerializeField] private string treeId;

    [SerializeField] private int maxHits = 3;
    [SerializeField] private float shakeDuration = 0.12f;
    [SerializeField] private float shakeStrength = 0.08f;

    [SerializeField] private GameObject intactSprite;
    [SerializeField] private GameObject choppedSprite;

    private int currentHits;
    private Vector3 originalPosition;
    private bool isChopped;

    private void Awake()
    {
        currentHits = maxHits;
        originalPosition = transform.localPosition;

        MaterialInfo.instance.RegisterTree(treeId);

        ApplyStateFromMaterialInfo();
    }

    public void ApplyStateFromMaterialInfo()
    {
        bool chopped = MaterialInfo.instance.IsTreeChopped(treeId);

        if (chopped)
        {
            isChopped = true;
            currentHits = 0;
            intactSprite.SetActive(false);
            choppedSprite.SetActive(true);
        }
        else
        {
            isChopped = false;
            currentHits = maxHits;
            intactSprite.SetActive(true);
            choppedSprite.SetActive(false);
        }
    }

    public void TakeHit()
    {
        if (isChopped)
            return;

        currentHits--;

        StartCoroutine(Shake());

        if (currentHits <= 0)
        {
            isChopped = true;
            intactSprite.SetActive(false);
            choppedSprite.SetActive(true);

            MaterialInfo.instance.SetTreeChopped(treeId, true);
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
