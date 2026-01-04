using TMPro;
using UnityEngine;

public class ShopToolDisplay : MonoBehaviour
{
    public PlayerController.ToolType toolType;
    public TMP_Text priceText;
    public GameObject buyButton;

    public void UpdateDisplay()
    {
        if (toolType == PlayerController.ToolType.axe)
        {
            if (SaveManager.instance.Data.hasAxe) SetOwned();
            else SetAvailable(ToolPriceController.instance.axePrice);
        }
        else if (toolType == PlayerController.ToolType.pickaxe)
        {
            if (SaveManager.instance.Data.hasPickaxe) SetOwned();
            else SetAvailable(ToolPriceController.instance.pickaxePrice);
        }
        else if (toolType == PlayerController.ToolType.fishingRod)
        {
            if (SaveManager.instance.Data.hasFishingRod) SetOwned();
            else SetAvailable(ToolPriceController.instance.fishingRodPrice);
        }
    }

    void SetOwned()
    {
        priceText.text = "OWNED";
        buyButton.SetActive(false);
    }

    void SetAvailable(float price)
    {
        priceText.text = "$" + price;
        buyButton.SetActive(true);
    }

    public void BuyTool()
    {
        if (toolType == PlayerController.ToolType.axe)
        {
            if (CurrencyController.instance.CheckMoney(ToolPriceController.instance.axePrice))
            {
                CurrencyController.instance.SpendMoney(ToolPriceController.instance.axePrice);
                SaveManager.instance.Data.hasAxe = true;
                UIController.instance.axeIcon.SetActive(true);
            }
        }
        else if (toolType == PlayerController.ToolType.pickaxe)
        {
            if (CurrencyController.instance.CheckMoney(ToolPriceController.instance.pickaxePrice))
            {
                CurrencyController.instance.SpendMoney(ToolPriceController.instance.pickaxePrice);
                SaveManager.instance.Data.hasPickaxe = true;
                UIController.instance.pickaxeIcon.SetActive(true);
            }
        }
        else if (toolType == PlayerController.ToolType.fishingRod)
        {
            if (CurrencyController.instance.CheckMoney(ToolPriceController.instance.fishingRodPrice))
            {
                CurrencyController.instance.SpendMoney(ToolPriceController.instance.fishingRodPrice);
                SaveManager.instance.Data.hasFishingRod = true;
                UIController.instance.fishingRodIcon.SetActive(true);
            }
        }

        UpdateDisplay();
    }
}
