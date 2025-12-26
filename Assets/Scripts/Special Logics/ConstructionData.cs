using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionData", menuName = "Game/Construction Data")]
public class ConstructionData : ScriptableObject
{
    public Construction.ConstructionType type;

    public float moneyCost;
    public int woodCost;
    public int stoneCost;

    public float dailyIncome;
}
