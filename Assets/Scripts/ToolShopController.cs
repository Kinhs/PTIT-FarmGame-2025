using UnityEngine;

public class ToolShopController : MonoBehaviour
{
    public ShopToolDisplay axeDisplay;
    public ShopToolDisplay pickaxeDisplay;

    public void OpenClose(PlayerController.ToolType toolType)
    {
        if (UIController.instance.theIC.gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gameObject.activeSelf == true)
            {
                UpdateAllDisplay(toolType);
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UpdateAllDisplay(PlayerController.ToolType toolType)
    {
        axeDisplay.gameObject.SetActive(false);
        pickaxeDisplay.gameObject.SetActive(false);

        if (toolType == PlayerController.ToolType.axe)
        {
            axeDisplay.gameObject.SetActive(true);
            axeDisplay.UpdateDisplay();
        }
        else if (toolType == PlayerController.ToolType.pickaxe)
        {
            pickaxeDisplay.gameObject.SetActive(true);
            pickaxeDisplay.UpdateDisplay();
        }
    }
}
