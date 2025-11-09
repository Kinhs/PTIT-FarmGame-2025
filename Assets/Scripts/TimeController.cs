using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (globalLight == null)
        {
            var foundLight = GameObject.FindWithTag("GlobalLight");
            if (foundLight != null)
                globalLight = foundLight.GetComponent<Light2D>();
        }
    }

    public float currentTime;
    public float dayStart, dayEnd;
    public float timeSpeed = .1f;
    private bool timeActive;
    public int currentDay = 1;
    public string dayEndScene;

    public Light2D globalLight;
    public float maxIntensity = 1f;
    public float minIntensity = 0.1f;
    public float morningTime = 6f;
    public float nightTime = 18f;
    public float middayStart = 12f;
    public float middayEnd = 14f;

    void Start()
    {
        currentTime = dayStart;
        timeActive = true;
    }

    void Update()
    {
        if (!timeActive)
            return;

        currentTime += Time.deltaTime * timeSpeed;

        if (currentTime > dayEnd)
        {
            currentTime = dayEnd;
            EndDay();
        }

        if (UIController.instance != null)
        {
            UIController.instance.UpdateTimeText(currentTime);
        }

        if (globalLight != null)
        {
            float intensity = minIntensity;

            if (currentTime >= morningTime && currentTime <= nightTime)
            {
                if (currentTime < middayStart)
                    intensity = Mathf.Lerp(minIntensity, maxIntensity, (currentTime - morningTime) / (middayStart - morningTime));
                else if (currentTime <= middayEnd)
                    intensity = maxIntensity;
                else
                    intensity = Mathf.Lerp(maxIntensity, minIntensity, (currentTime - middayEnd) / (nightTime - middayEnd));
            }
            else
            {
                intensity = minIntensity;
            }

            if (Mathf.Abs(globalLight.intensity - intensity) > 0.001f)
                globalLight.intensity = intensity;
        }
    }

    public void EndDay()
    {
        timeActive = false;
        currentDay++;

        if (GridInfo.instance != null)
        {
            GridInfo.instance.GrowCrop();
        }

        PlayerPrefs.SetString("Transition", "Wake Up");
        SceneManager.LoadScene(dayEndScene);
    }

    public void StartDay()
    {
        timeActive = true;
        currentTime = dayStart;
        AudioManager.instance.PlaySFXPitchAdjusted(7);
    }
}
