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

        UpdateDisplay();
    }
}
