using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Construction : MonoBehaviour
{
    public enum ConstructionType
    {
        Well,
        Windmill,
        Greenhouse,
        Hydroelectric
    }

    public ConstructionType constructionType;

    public GameObject builtObject;
    public GameObject unbuiltObject;
    public GameObject infoObject;

    public TextMeshProUGUI moneyCostText;
    public TextMeshProUGUI woodCostText;
    public TextMeshProUGUI stoneCostText;
    public TextMeshProUGUI incomeText;

    private bool isBuilt;
    private bool playerInRange;
    private ConstructionData data;

    private void Start()
    {
        data = ConstructionDatabase.instance.GetData(constructionType);

        isBuilt = false;

        builtObject.SetActive(false);
        unbuiltObject.SetActive(true);
        infoObject.SetActive(false);

        UpdateInfoUI();

        if ((constructionType == ConstructionType.Well && SaveManager.instance.Data.builtWell == true)
        || (constructionType == ConstructionType.Windmill && SaveManager.instance.Data.builtWindmill == true)
        || (constructionType == ConstructionType.Greenhouse && SaveManager.instance.Data.builtGreenhouse == true)
        || (constructionType == ConstructionType.Hydroelectric && SaveManager.instance.Data.builtHydroelectric == true))
        {
            Build();
        }
    }

    private void Update()
    {
        if (!playerInRange || isBuilt)
            return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryBuild();
        }
    }

    private void UpdateInfoUI()
    {
        moneyCostText.text = data.moneyCost + " $";
        woodCostText.text = data.woodCost.ToString();
        stoneCostText.text = data.stoneCost.ToString();
        incomeText.text = "+ $" + data.dailyIncome + "/day";
    }

    private void TryBuild()
    {
        if (!CurrencyController.instance.CheckMoney(data.moneyCost))
            return;

        if (MaterialController.instance.woodAmount < data.woodCost)
            return;

        if (MaterialController.instance.stoneAmount < data.stoneCost)
            return;

        CurrencyController.instance.SpendMoney(data.moneyCost);
        MaterialController.instance.woodAmount -= data.woodCost;
        MaterialController.instance.stoneAmount -= data.stoneCost;
        //PlayerController.instance.GetTired(100);

        if (constructionType == ConstructionType.Well) SaveManager.instance.Data.builtWell = true;
        else if (constructionType == ConstructionType.Windmill) SaveManager.instance.Data.builtWindmill = true;
        else if (constructionType == ConstructionType.Greenhouse) SaveManager.instance.Data.builtGreenhouse= true;
        else if (constructionType == ConstructionType.Hydroelectric) SaveManager.instance.Data.builtHydroelectric = true;

        Build();
    }

    private void Build()
    {
        isBuilt = true;

        builtObject.SetActive(true);
        unbuiltObject.SetActive(false);
        infoObject.SetActive(false);
    }

    public float GetDailyIncome()
    {
        return data.dailyIncome;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            infoObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            infoObject.SetActive(false);
        }
    }
}
