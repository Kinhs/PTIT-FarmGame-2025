using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToStart;

    public GameObject tutorialPopup;
    public GameObject restartPopup;

    private void Start()
    {
        AudioManager.instance.PlayTitle();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart);

        AudioManager.instance.PlayNextBGM();
        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game...");
        AudioManager.instance.PlaySFXPitchAdjusted(6);
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

    public void ShowHideRestart()
    {
        if (restartPopup.activeSelf == false)
        {
            restartPopup.SetActive(true);
        }
        else
        {
            restartPopup.SetActive(false);
        }
        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void ShowHideSetting()
    {

        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }

    public void RestartGame()
    {
        SaveManager.instance.Data = new();
        SaveManager.instance.SaveGame();
    }
}
