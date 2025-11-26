using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopFishDisplay : MonoBehaviour
{
    public FishController.FishType fish;
    public Image fishImage;
    public TMP_Text amountText, priceText;

    public void UpdateDisplay()
    {
        FishInfo info = FishController.instance.GetFishInfo(fish);

        fishImage.sprite = info.sprite;
        amountText.text = "x" + info.amount;

        priceText.text = "$" + info.price + " each";
    }
}
