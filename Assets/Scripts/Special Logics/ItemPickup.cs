using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    private Transform target;
    private float flyDuration;
    private System.Action onArrive;

    public void FlyTo(Transform target, float duration, System.Action onArrive)
    {
        this.target = target;
        this.flyDuration = duration;
        this.onArrive = onArrive;

        StartCoroutine(FlyRoutine());
    }

    private IEnumerator FlyRoutine()
    {
        Vector3 startPos = transform.position;
        float time = 0f;

        while (time < flyDuration)
        {
            time += Time.deltaTime;
            float t = time / flyDuration;
            transform.position = Vector3.Lerp(startPos, target.position, t);
            yield return null;
        }

        onArrive?.Invoke();
        gameObject.SetActive(false);
    }
}
