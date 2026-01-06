using UnityEngine;
using System.Collections;

public class FishingRodController : MonoBehaviour
{
    public Transform rodRoot;
    public Transform hookTip;

    public float castTime = 0.5f;
    public float retractTime = 0.5f;

    public float biteTimeMin = 2f;
    public float biteTimeMax = 5f;

    public Transform biteCircle;
    public Transform caughtFishSprite;

    public float biteCircleStartAlpha = 0.5f;
    public Vector3 biteCircleStartScale = Vector3.one;

    public bool isCast { get; private set; }
    public bool canCatch { get; private set; }
    public bool canRetract { get; private set; }

    [HideInInspector] public bool isInBonusZone;

    public float maxLength = 20.0f;
    public float minLength = 5.0f;

    Coroutine moveRoutine;
    Coroutine biteRoutine;

    SpriteRenderer biteCircleRenderer;
    SpriteRenderer caughtFishRenderer;

    FishInfo selectedFish;

    void Awake()
    {
        if (biteCircle != null)
            biteCircleRenderer = biteCircle.GetComponent<SpriteRenderer>();

        if (caughtFishSprite != null)
            caughtFishRenderer = caughtFishSprite.GetComponent<SpriteRenderer>();
    }

    public void Cast(Vector3 target)
    {
        isCast = true;
        canRetract = true;
        ResetBite();

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveHook(rodRoot.position, target, castTime, true));

        AudioManager.instance.PlaySFX(9);
    }

    public void Retract()
    {
        if (!isCast) return;

        canRetract = false;

        if (canCatch && selectedFish != null)
        {
            caughtFishSprite.gameObject.SetActive(true);
            caughtFishSprite.position = hookTip.position;
            caughtFishSprite.localScale = new Vector3(selectedFish.size, selectedFish.size, 1f);
            caughtFishRenderer.sprite = selectedFish.sprite;

            FishController.instance.AddFish(selectedFish.fishType);
        }

        ResetBite();

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveHook(hookTip.position, rodRoot.position, retractTime, false));

        AudioManager.instance.PlaySFX(10);
    }

    IEnumerator MoveHook(Vector3 start, Vector3 end, float time, bool isCastingAction)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / time);
            hookTip.position = Vector3.Lerp(start, end, k);
            yield return null;
        }

        hookTip.position = end;

        if (isCastingAction)
        {
            isCast = true;
            selectedFish = GetRandomFish();
            biteRoutine = StartCoroutine(BiteProcess());
        }
        else
        {
            isCast = false;
            caughtFishSprite.gameObject.SetActive(false);
        }
    }

    IEnumerator BiteProcess()
    {
        float waitTime = Random.Range(biteTimeMin, biteTimeMax);
        if (isInBonusZone)
        {
            waitTime /= 2;
        }

        yield return new WaitForSeconds(waitTime);

        if (selectedFish == null)
        {
            canCatch = false;
            yield break;
        }

        float duration = selectedFish.catchTime;

        biteCircle.gameObject.SetActive(true);
        biteCircle.localScale = biteCircleStartScale;
        AudioManager.instance.PlaySFX(8);

        if (biteCircleRenderer != null)
        {
            Color c = biteCircleRenderer.color;
            c.a = biteCircleStartAlpha;
            biteCircleRenderer.color = c;
        }

        float t = 0f;
        canCatch = false;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / duration);

            biteCircle.localScale = Vector3.Lerp(biteCircleStartScale, Vector3.zero, k);

            if (biteCircleRenderer != null)
            {
                Color c = biteCircleRenderer.color;
                c.a = Mathf.Lerp(biteCircleStartAlpha, 1.0f, k);
                biteCircleRenderer.color = c;
            }

            if (t >= duration * 0.8f)
                canCatch = true;

            yield return null;
        }

        biteCircle.gameObject.SetActive(false);
        canCatch = false;
    }

    void ResetBite()
    {
        canCatch = false;
        selectedFish = null;

        if (biteRoutine != null)
            StopCoroutine(biteRoutine);

        biteCircle.gameObject.SetActive(false);
    }

    FishInfo GetRandomFish()
    {
        var list = FishController.instance.fishList;
        if (list.Count == 0) return null;

        float total = 0f;
        for (int i = 0; i < list.Count; i++)
            total += list[i].commonPoint;

        float rnd = Random.Range(0f, total);
        float sum = 0f;

        for (int i = 0; i < list.Count; i++)
        {
            sum += list[i].commonPoint;
            if (rnd <= sum)
                return list[i];
        }

        return list[list.Count - 1];
    }
}
