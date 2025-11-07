using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToStart;

    public GameObject tutorialPopup;

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

    public void ShowHideSetting()
    {

        AudioManager.instance.PlaySFXPitchAdjusted(6);
    }
}
