using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject[] toolbarActivatorIcons;

    public TMP_Text timeText;

    public InventoryController theIC;
    public ShopController theShop;
    public FishShopController fishShop;

    public Image seedImage;

    public TMP_Text moneyText;

    public GameObject pauseScreen;

    public string mainMenuScene;

    public Image fadeScreen;
    public float fadeSpeed = 2f;

    public GameObject tutorialPopup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            theIC.OpenClose();
        }

#if UNITY_EDITOR
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            theShop.OpenClose();
        }
#endif

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseUnpause();
        }
    }

    public void SwitchTool(int selected)
    {
        foreach(GameObject icon in toolbarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolbarActivatorIcons[selected].SetActive(true);
    }

    public void UpdateTimeText(float currentTime)
    {
        if (currentTime < 12)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + "AM";
        } else if (currentTime < 13)
        {
            timeText.text = "12PM";
        } else if (currentTime < 24)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12) + "PM";
        } else if (currentTime < 25)
        {
            timeText.text = "12AM";
        } else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24) + "AM";
        }
    }

    public void SwitchSeed(CropController.CropType crop)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(crop).seedType;
        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void UpdateMoneyText(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(mainMenuScene);

        Destroy(gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GridInfo.instance.gameObject);
        Destroy(TimeController.instance.gameObject);
        Destroy(CropController.instance.gameObject);
        Destroy(CurrencyController.instance.gameObject);

        AudioManager.instance.PlayTitle();
        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public IEnumerator FadeIn()
    {
        Color originalColor = fadeScreen.color;
        originalColor.a = 1f;
        fadeScreen.color = originalColor;

        while (fadeScreen.color.a > 0f)
        {
            originalColor.a -= fadeSpeed * Time.deltaTime;
            fadeScreen.color = originalColor;
            yield return null;
        }

        originalColor.a = 0f;
        fadeScreen.color = originalColor;
        fadeScreen.gameObject.SetActive(false);
    }

    public void ShowHideTutorial()
    {
        if (tutorialPopup.activeSelf == false)
        {
            tutorialPopup.SetActive(true);
        }
        else
        {
            tutorialPopup.SetActive(false);
        }
        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void ShowHideSetting()
    {

        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }
}
