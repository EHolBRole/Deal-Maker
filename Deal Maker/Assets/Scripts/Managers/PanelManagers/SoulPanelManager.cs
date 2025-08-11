using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPanelManager : MonoBehaviour
{
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI availableText;

    public TextMeshProUGUI militaryText;
    public TextMeshProUGUI spyText;
    public TextMeshProUGUI industryText;

    public Button addMilitaryButton;
    public Button removeMilitaryButton;

    public Button addSpyButton;
    public Button removeSpyButton;

    public Button addIndustryButton;
    public Button removeIndustryButton;


    public GameObject soulsPanel;

    public SoulsManager soulManager;
    public PlayerDemonController playerController;

    void Start()
    {
        addMilitaryButton.onClick.AddListener(() => Allocate("military", 1));
        removeMilitaryButton.onClick.AddListener(() => Unallocate("military", 1));

        addSpyButton.onClick.AddListener(() => Allocate("spy", 1));
        removeSpyButton.onClick.AddListener(() => Unallocate("spy", 1));

        addIndustryButton.onClick.AddListener(() => Allocate("industry", 1));
        removeIndustryButton.onClick.AddListener(() => Unallocate("industry", 1));

        UpdatePanel();
    }

    void Allocate(string type, int amount)
    {
        if (soulManager.AllocateSouls(type, amount)) UpdatePanel();
    }

    void Unallocate(string type, int amount)
    {
        if (soulManager.UnallocateSouls(type, amount)) UpdatePanel();
    }

    public void UpdatePanel()
    {
        totalText.text = $"Total Souls: {playerController.player.souls}";
        availableText.text = $"Available Souls: {soulManager.availableSouls}";
        militaryText.text = $"Military Souls: {soulManager.militarySouls}";
        spyText.text = $"Spy Souls: {soulManager.spySouls}";
        industryText.text = $"Industry Souls: {soulManager.industrySouls}";
    }

    public void ToggleSoulsMenu() => soulsPanel.SetActive(!soulsPanel.activeSelf);
}
