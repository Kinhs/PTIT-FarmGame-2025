using UnityEngine;

public class FishingBonusZone : MonoBehaviour
{
    public Transform zone;
    public float activeMin = 1f;
    public float activeMax = 3f;
    public float inactiveMin = 1f;
    public float inactiveMax = 3f;

    public float maxAlpha = 1f;
    public float fadeSpeed = 2f;

    private float timer;
    private bool isActiveState = false;
    private SpriteRenderer sr;
    private float currentAlpha = 0f;

    private void Start()
    {
        sr = zone.GetComponent<SpriteRenderer>();
        SetAlpha(0f);
        timer = Random.Range(inactiveMin, inactiveMax);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActiveState = !isActiveState;

            if (isActiveState)
                timer = Random.Range(activeMin, activeMax);
            else
                timer = Random.Range(inactiveMin, inactiveMax);
        }

        if (isActiveState)
        {
            zone.gameObject.SetActive(true);
            currentAlpha = Mathf.MoveTowards(currentAlpha, maxAlpha, fadeSpeed * Time.deltaTime);
        }
        else
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, 0f, fadeSpeed * Time.deltaTime);
            if (currentAlpha <= 0.1f) zone.gameObject.SetActive(false);
        }

        SetAlpha(currentAlpha);
    }

    private void SetAlpha(float a)
    {
        if (sr)
        {
            var c = sr.color;
            c.a = a;
            sr.color = c;
        }
    }
}
