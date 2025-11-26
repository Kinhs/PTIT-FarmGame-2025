using UnityEngine;
using System.Collections;

public class FishingRodController : MonoBehaviour
{
    public Transform rodRoot;
    public Transform hookTip;

    public float castTime = 0.5f;
    public float retractTime = 0.5f;

    public bool isCast { get; private set; }

    Vector3 castTarget;
    Coroutine moveRoutine;

    public void Cast(Vector3 target)
    {
        castTarget = target;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        isCast = true;
        moveRoutine = StartCoroutine(MoveHook(rodRoot.position, target, castTime, true));
    }

    public void Retract()
    {
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
            isCast = true;
        else
            isCast = false;
    }
}
