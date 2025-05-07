using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeData upgradeData;
    public UpgradeManager upgradeManager;
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCostText;
        
    void Start()
    {
        // Setup button texts
        upgradeNameText.text = upgradeData.upgradeName;
        upgradeDescriptionText.text = upgradeData.description;
        upgradeCostText.text = $"Cost: {upgradeData.costSoulCoins} Souls, {upgradeData.costInfluence} Influence, {upgradeData.costSecrets} Secrets";
    }

    public void OnUpgradeButtonClicked()
    {
        upgradeManager.ApplyUpgrade(upgradeData);
    }
}
