using UnityEngine;

public class EndGameScreenController : MonoBehaviour
{
    public static EndGameScreenController instance;

    public GameObject endGameScreen;
    public bool hasShown;

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

    public bool IsGoalReached()
    {
        var data = SaveManager.instance.Data;

        return data.builtWell
            && data.builtWindmill
            && data.builtGreenhouse
            && data.builtHydroelectric
            && data.money >= 10000f;
    }

    public void ShowEndGameScreen()
    {
        if (hasShown) return;

        endGameScreen.SetActive(true);
        hasShown = true;
    }

    public void CloseEndGameScreen()
    {
        endGameScreen.SetActive(false);
    }
}
