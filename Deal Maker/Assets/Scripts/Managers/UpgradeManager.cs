using System.Resources;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStatsManager playerStats;
    public ResourceManager resourceManager; // For checking if the player has enough resources

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        // Check if the player has enough resources
        if (resourceManager.soulCoins >= upgrade.costSoulCoins &&
            resourceManager.influence >= upgrade.costInfluence &&
            resourceManager.secrets >= upgrade.costSecrets)
        {
            // Deduct the costs
            resourceManager.ChangeSoulCoins(-upgrade.costSoulCoins);
            resourceManager.ChangeInfluence(-upgrade.costInfluence);
            resourceManager.ChangeSecrets(-upgrade.costSecrets);

            // Apply the upgrade
            if (upgrade.statType == "Leadership")
            {
                playerStats.IncreaseLeadership(upgrade.statIncrease);
            }
            else if (upgrade.statType == "Charisma")
            {
                playerStats.IncreaseCharisma(upgrade.statIncrease);
            }
            else if (upgrade.statType == "Action Slots")
            {
                playerStats.UpgradeActionSlots(upgrade.statIncrease);
            }
            else if (upgrade.statType == "Soul Capacity")
            {
                playerStats.UpgradeSoulCapacity(upgrade.statIncrease);
            }   
            else if (upgrade.statType == "Deal Slot Limit")
            {
                playerStats.UpgradeDealSlotLimit(upgrade.statIncrease);
            }

            Debug.Log("Upgrade applied: " + upgrade.upgradeName);
        }
        else
        {
            Debug.Log("Not enough resources for upgrade: " + upgrade.upgradeName);
        }
    }
}
