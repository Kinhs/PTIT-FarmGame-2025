using UnityEngine;
using System.Collections;

public class WoodTree : MonoBehaviour
{
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

        intactSprite.SetActive(true);
        choppedSprite.SetActive(false);
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
