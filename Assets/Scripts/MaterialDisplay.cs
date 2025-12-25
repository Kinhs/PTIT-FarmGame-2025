using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialDisplay : MonoBehaviour
{
    public TMP_Text woodAmountText;
    public TMP_Text stoneAmountText;

    public void UpdateDisplay()
    {
        woodAmountText.text = "x" + MaterialController.instance.woodAmount;
        stoneAmountText.text = "x" + MaterialController.instance.stoneAmount;
    }
}
