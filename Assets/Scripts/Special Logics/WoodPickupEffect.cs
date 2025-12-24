using UnityEngine;

public class WoodPickupEffect : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float duration;
    private float timer;
    private bool active;

    public void Play(Vector3 from, Vector3 to, float flyDuration)
    {
        startPos = from;
        targetPos = to;
        duration = flyDuration;
        timer = 0f;
        active = true;

        transform.position = from;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!active)
            return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        transform.position = Vector3.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            active = false;
            gameObject.SetActive(false);
        }
    }
}
