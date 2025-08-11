using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradePanelManager : MonoBehaviour
{
    public GameObject upgradePanel;  // The panel that holds all upgrade buttons
    public GameObject upgradeButtonPrefab;  // The button prefab to instantiate for each upgrade
    public Transform upgradeButtonContainer;  // The container where upgrade buttons will be added

    public UpgradeManager upgradeManager;  // Reference to the upgrade manager
    public List<UpgradeData> availableUpgrades;  // List of all available upgrades

    public void ToggleUpgradePanel()
    {
        // Toggle the visibility of the upgrade panel
        upgradePanel.SetActive(!upgradePanel.activeSelf);

        // Populate the panel with available upgrade buttons
        if (upgradePanel.activeSelf)
        {
            PopulateUpgradeButtons();
        }
    }

    void PopulateUpgradeButtons()
    {
        // Clear any existing buttons in the container
        foreach (Transform child in upgradeButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a button for each upgrade in the available upgrades list
        foreach (UpgradeData upgrade in availableUpgrades)
        {
            GameObject upgradeButton = Instantiate(upgradeButtonPrefab, upgradeButtonContainer);
            UpgradeButton buttonScript = upgradeButton.GetComponent<UpgradeButton>();

            // Set up the button's UI elements (name, description, cost)
            buttonScript.upgradeData = upgrade;
            buttonScript.upgradeManager = upgradeManager;

            // Set the button's texts
            TextMeshProUGUI nameText = upgradeButton.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>();
            nameText.text = upgrade.upgradeName;

            TextMeshProUGUI descriptionText = upgradeButton.transform.Find("UpgradeDescription").GetComponent<TextMeshProUGUI>();
            descriptionText.text = upgrade.description;

            TextMeshProUGUI costText = upgradeButton.transform.Find("UpgradeCost").GetComponent<TextMeshProUGUI>();
            costText.text = $"Cost: {upgrade.costSoulCoins} Souls, {upgrade.costInfluence} Influence, {upgrade.costSecrets} Secrets";
        }
    }

}
