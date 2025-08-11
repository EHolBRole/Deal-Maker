using System.Resources;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStatsManager playerStats;
    public ResourceManager resourceManager;
    public PlayerDemonController playerController; // For checking if the player has enough resources

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        // Check if the player has enough resources
        if (playerController.player.soulCoins >= upgrade.costSoulCoins &&
            playerController.player.influence >= upgrade.costInfluence &&
            playerController.player.secrets >= upgrade.costSecrets)
        {
            // Deduct the costs
            resourceManager.ChangeSoulCoins(-upgrade.costSoulCoins);
            resourceManager.ChangeInfluence(-upgrade.costInfluence);
            resourceManager.ChangeSecrets(-upgrade.costSecrets);

            // Apply the upgrade
            switch (upgrade.statType)
            {
                case "Leadership":
                    playerStats.IncreaseLeadership(upgrade.statIncrease);
                    break;
                case "Charisma":
                    playerStats.IncreaseCharisma(upgrade.statIncrease);
                    break;
                case "Action Slots":
                    playerStats.UpgradeActionSlots(upgrade.statIncrease);
                    break;
                case "Soul Capacity":
                    playerStats.UpgradeSoulCapacity(upgrade.statIncrease);
                    break;
                case "Deal Slot Limit":
                    playerStats.UpgradeDealSlotLimit(upgrade.statIncrease);
                    break;
                default:
                    Debug.Log("Something went wrong with upgrade Stat type!");
                    break;
            }
            Debug.Log("Upgrade applied: " + upgrade.upgradeName);
        }
        else
        {
            Debug.Log("Not enough resources for upgrade: " + upgrade.upgradeName);
        }
    }
}
