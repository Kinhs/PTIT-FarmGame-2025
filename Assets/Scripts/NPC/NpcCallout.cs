using UnityEngine;

public class NpcCallout : MonoBehaviour
{
    public GameObject[] calloutObjects;
    public float minInterval = 4f;
    public float maxInterval = 7f;
    public float displayDuration = 3f;

    private float timer;
    private float nextTime;
    private float displayTimer;
    private int currentIndex = -1;
    private int lastIndex = -1;

    void Start()
    {
        if (calloutObjects == null || calloutObjects.Length == 0)
        {
            Debug.LogError("NpcCallout: calloutObjects is not assigned");
            enabled = false;
            return;
        }

        for (int i = 0; i < calloutObjects.Length; i++)
        {
            if (calloutObjects[i] != null)
                calloutObjects[i].SetActive(false);
        }

        nextTime = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        if (currentIndex == -1)
        {
            timer += Time.deltaTime;
            if (timer >= nextTime)
            {
                int index;
                do
                {
                    index = Random.Range(0, calloutObjects.Length);
                }
                while (index == lastIndex && calloutObjects.Length > 1);

                if (calloutObjects[index] == null)
                {
                    timer = 0f;
                    nextTime = Random.Range(minInterval, maxInterval);
                    return;
                }

                lastIndex = index;
                currentIndex = index;
                calloutObjects[currentIndex].SetActive(true);

                displayTimer = 0f;
                timer = 0f;
            }
        }
        else
        {
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayDuration)
            {
                if (calloutObjects[currentIndex] != null)
                    calloutObjects[currentIndex].SetActive(false);

                currentIndex = -1;
                nextTime = Random.Range(minInterval, maxInterval);
            }
        }
    }
}
