using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{

    public PlayerDemonController playerController;

    public void IncreaseLeadership(int amount = 1) => playerController.player.leadership += amount;
    public void IncreaseCharisma(int amount = 1) => playerController.player.charisma += amount;

    public int GetLeadership() => playerController.player.leadership;
    public int GetCharisma() => playerController.player.charisma;


    // === Domain Stats ===
    [Header("Domain Stats")]
    public int actionSlots = 1;          // How many Actions per turn
    public int soulCapacity = 10;        // Max number of Souls you can own
    public int dealSlotLimit = 3;        // How many Deals can be stored

    public void UpgradeActionSlots(int amount = 1) => actionSlots += amount;
    public void UpgradeSoulCapacity(int amount = 5) => soulCapacity += amount;
    public void UpgradeDealSlotLimit(int amount = 1) => dealSlotLimit += amount;

    public int GetActionSlots() => actionSlots;
    public int GetSoulCapacity() => soulCapacity;
    public int GetDealSlotLimit() => dealSlotLimit;
}
