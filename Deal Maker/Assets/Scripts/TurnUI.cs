using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnUI : MonoBehaviour
{
    public TurnManager turnManager;

    public TMP_Text weekText;
    public TMP_Text majorActionText;
    public TMP_Text minorActionText;

    void Update()
    {
        weekText.text = "Week: " + turnManager.currentWeek;
        majorActionText.text = "Major Actions Left: " +
            (turnManager.maxMajorActions - turnManager.GetMajorActionsUsed());
        minorActionText.text = "Minor Actions Left: " +
            (turnManager.maxMinorActions - turnManager.GetMinorActionsUsed());
    }
}
