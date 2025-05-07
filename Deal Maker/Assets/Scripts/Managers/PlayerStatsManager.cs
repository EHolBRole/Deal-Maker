using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // === Demon Stats ===
    [Header("Demon Stats")]
    public int leadership = 1;
    public int charisma = 1;

    public void IncreaseLeadership(int amount = 1) => leadership += amount;
    public void IncreaseCharisma(int amount = 1) => charisma += amount;

    public int GetLeadership() => leadership;
    public int GetCharisma() => charisma;


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
