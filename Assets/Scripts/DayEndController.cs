using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DayEndController : MonoBehaviour
{
    public static float baseIncome = 10f;

    public TMP_Text dayText;
    public TMP_Text incomeText;

    public string wakeUpScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TimeController.instance != null)
        {
            dayText.text = "- Day " + TimeController.instance.currentDay + " -";
        }

        float income = CalculateIncome();
        incomeText.text = "+ $" + income;

        CurrencyController.instance.AddMoney(income);

        AudioManager.instance.PlaySFXPitchAdjusted(5);

        SaveManager.instance.Data.money = CurrencyController.instance.currentMoney;
        CropController.instance.SaveToSaveManager();
        FishController.instance.SaveToSaveManager();
        MaterialController.instance.SaveToSaveManager();
        GridInfo.instance.SaveToSaveManager();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            TimeController.instance.StartDay();

            SceneManager.LoadScene(wakeUpScene);
        }
    }

    float CalculateIncome()
    {
        float result = baseIncome;

        if (SaveManager.instance.Data.builtWell)
            result += ConstructionDatabase.instance.GetData(Construction.ConstructionType.Well).dailyIncome;
        if (SaveManager.instance.Data.builtWindmill)
            result += ConstructionDatabase.instance.GetData(Construction.ConstructionType.Windmill).dailyIncome;
        if (SaveManager.instance.Data.builtGreenhouse)
            result += ConstructionDatabase.instance.GetData(Construction.ConstructionType.Greenhouse).dailyIncome;
        if (SaveManager.instance.Data.builtHydroelectric)
            result += ConstructionDatabase.instance.GetData(Construction.ConstructionType.Hydroelectric).dailyIncome;

        return result;
    }
}
