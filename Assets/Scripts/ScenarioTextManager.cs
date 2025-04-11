using TMPro; // Add this to access TextMeshPro types
using UnityEngine;

public class ScenarioTextManager : MonoBehaviour
{
    public TMP_Text[] scenarioTexts; // Update type to TMP_Text

    public void ShowText(int index)
    {
        // Hide all texts
        foreach (var text in scenarioTexts)
        {
            text.gameObject.SetActive(false);
        }

        // Show the specified text if the index is valid
        if (index >= 0 && index < scenarioTexts.Length)
        {
            scenarioTexts[index].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid scenario index: " + index);
        }
    }
}

