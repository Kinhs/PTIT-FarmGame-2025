using UnityEngine;

public class HouseVisuals : MonoBehaviour
{
    [Header("House Stages")]
    public GameObject[] houseLevels; // Drag your Levels here in the Inspector
    private int currentLevel = 2;

    void Start()
    {
        UpdateHouseAppearance();
    }

    public void Upgrade()
    {
        if (currentLevel < houseLevels.Length - 1)
        {
            currentLevel++;
            UpdateHouseAppearance();
            Debug.Log("House Upgraded to Level: " + currentLevel);
        }
        else
        {
            Debug.Log("House is already at Max Level!");
        }
    }

    void UpdateHouseAppearance()
    {
        for (int i = 0; i < houseLevels.Length; i++)
        {
            // Only the current level should be active
            houseLevels[i].SetActive(i == currentLevel);
        }
    }
}
