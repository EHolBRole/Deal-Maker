using UnityEngine;


[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade System/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;

    public int costSoulCoins;  // SoulCoins cost for the upgrade
    public int costInfluence;  // Influence cost for the upgrade
    public int costSecrets;  // Secrets cost for the upgrade

    public int statIncrease;  // How much the stat increases by
    public string statType;  // Type of stat to upgrade (e.g., "Leadership", "Charisma")
}
