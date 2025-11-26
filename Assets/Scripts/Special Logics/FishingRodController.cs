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

    public float biteDuration = 2f;

    public Transform biteCircle;
    public Transform caughtFishSprite;

    public float biteCircleStartAlpha = 0.5f;
    public Vector3 biteCircleStartScale = Vector3.one;

    public bool isCast { get; private set; }
    public bool canCatch { get; private set; }

    Vector3 castTarget;
    Coroutine moveRoutine;
    Coroutine biteRoutine;

    SpriteRenderer biteCircleRenderer;

    void Awake()
    {
        if (biteCircle != null)
            biteCircleRenderer = biteCircle.GetComponent<SpriteRenderer>();
    }

    public void Cast(Vector3 target)
    {
        isCast = true;
        castTarget = target;
        ResetBite();
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveHook(rodRoot.position, target, castTime, true));
    }

    public void Retract()
    {
        if (!isCast) return;

        if (canCatch)
        {
            caughtFishSprite.gameObject.SetActive(true);
            caughtFishSprite.position = hookTip.position;
        }

        ResetBite();

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveHook(hookTip.position, rodRoot.position, retractTime, false));
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
        yield return new WaitForSeconds(waitTime);

        biteCircle.gameObject.SetActive(true);
        biteCircle.localScale = biteCircleStartScale;
        if (biteCircleRenderer != null)
        {
            Color c = biteCircleRenderer.color;
            c.a = biteCircleStartAlpha;
            biteCircleRenderer.color = c;
        }

        float t = 0f;
        canCatch = false;

        while (t < biteDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / biteDuration);
            biteCircle.localScale = Vector3.Lerp(biteCircleStartScale, Vector3.zero, k);

            if (biteCircleRenderer != null)
            {
                Color c = biteCircleRenderer.color;
                c.a = Mathf.Lerp(biteCircleStartAlpha, 1.5f, k);
                biteCircleRenderer.color = c;
            }

            if (t >= biteDuration * 0.8f)
                canCatch = true;

            yield return null;
        }

        biteCircle.gameObject.SetActive(false);
        canCatch = false;
    }

    void ResetBite()
    {
        canCatch = false;

        if (biteRoutine != null)
            StopCoroutine(biteRoutine);

        biteCircle.gameObject.SetActive(false);
    }
}
