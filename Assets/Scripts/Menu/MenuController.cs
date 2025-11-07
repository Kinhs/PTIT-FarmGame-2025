using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToStart;

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game...");
    }

    public void ShowTutorial()
    {

    }

    public void ShowSetting()
    {

    }
}
