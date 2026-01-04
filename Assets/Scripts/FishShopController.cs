using UnityEngine;

public class FishShopController : MonoBehaviour
{
    public ShopFishDisplay[] fishes;
    public ShopToolDisplay fishingRod;

    public void OpenClose()
    {
        if (UIController.instance.theIC.gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gameObject.activeSelf == true)
            {
                UpdateAllDisplay();
            }
        }
    }

    public void UpdateAllDisplay()
    {
        foreach (ShopFishDisplay fish in fishes)
        {
            fish.UpdateDisplay();
        }
        fishingRod.UpdateDisplay();
    }
}
