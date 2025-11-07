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
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game...");
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
    }

    public void ShowHideSetting()
    {

    }
}
