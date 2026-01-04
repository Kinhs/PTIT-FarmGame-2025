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
            if (PlayerController.instance.hasAxe) SetOwned();
            else SetAvailable(ToolPriceController.instance.axePrice);
        }
        else if (toolType == PlayerController.ToolType.pickaxe)
        {
            if (PlayerController.instance.hasPickaxe) SetOwned();
            else SetAvailable(ToolPriceController.instance.pickaxePrice);
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
                PlayerController.instance.hasAxe = true;
                UIController.instance.axeIcon.SetActive(true);
            }
        }
        else if (toolType == PlayerController.ToolType.pickaxe)
        {
            if (CurrencyController.instance.CheckMoney(ToolPriceController.instance.pickaxePrice))
            {
                CurrencyController.instance.SpendMoney(ToolPriceController.instance.pickaxePrice);
                PlayerController.instance.hasPickaxe = true;
                UIController.instance.pickaxeIcon.SetActive(true);
            }
        }

        UpdateDisplay();
    }
}
