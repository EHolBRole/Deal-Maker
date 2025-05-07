using UnityEngine;
using TMPro;

public class StatsPanelManager : MonoBehaviour
{
    public PlayerStatsManager playerStats;

    [Header("Demon Stats")]
    public TextMeshProUGUI leadershipText;
    public TextMeshProUGUI charismaText;

    [Header("Domain Stats")]
    public TextMeshProUGUI actionSlotsText;
    public TextMeshProUGUI soulCapacityText;
    public TextMeshProUGUI dealSlotLimitText;

    public GameObject panel; // Optional: assign panel for toggling visibility

    void Start()
    {
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        leadershipText.text = $"Leadership: {playerStats.GetLeadership()}";
        charismaText.text = $"Charisma: {playerStats.GetCharisma()}";

        actionSlotsText.text = $"Action Slots: {playerStats.GetActionSlots()}";
        soulCapacityText.text = $"Soul Capacity: {playerStats.GetSoulCapacity()}";
        dealSlotLimitText.text = $"Deal Slot Limit: {playerStats.GetDealSlotLimit()}";
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
        if (panel.activeSelf) UpdateStatsUI();
    }
}
