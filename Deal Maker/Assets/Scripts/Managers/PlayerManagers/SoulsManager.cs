using System.Collections.Generic;
using UnityEngine;

public class SoulsManager : MonoBehaviour
{
    [Header("Soul Count")]
    public int availableSouls = 0; // Free souls ready to be allocated

    [Header("Allocation")]
    public int militarySouls = 0;
    public int spySouls = 0;
    public int industrySouls = 0;

    [Header("Unique Souls")]
    public List<string> uniqueSoulNames = new List<string>(); // Simple version: store names or IDs
    
    public PlayerDemonController playerController;
    public SoulPanelManager manager;

    public void Start()
    {
        availableSouls = playerController.player.souls;
    }

    // === Modify Souls ===
    public void AddSouls(int amount)
    {
        playerController.player.souls += amount;
        availableSouls += amount;
        manager.UpdatePanel();
    }

    public bool RemoveSouls(int amount)
    {
        if (availableSouls >= amount)
        {
            playerController.player.souls -= amount;
            availableSouls -= amount;
            manager.UpdatePanel();
            return true;
        }
        Debug.LogWarning("Not enough available souls to remove.");
        return false;
    }

    // === Allocate Souls ===
    public bool AllocateSouls(string type, int amount)
    {
        if (availableSouls < amount) return false;

        switch (type.ToLower())
        {
            case "military":
                militarySouls += amount;
                break;
            case "spy":
                spySouls += amount;
                break;
            case "industry":
                industrySouls += amount;
                break;
            default:
                Debug.LogWarning("Unknown allocation type.");
                return false;
        }

        availableSouls -= amount;
        return true;
    }

    // === Unallocate ===
    public bool UnallocateSouls(string type, int amount)
    {
        switch (type.ToLower())
        {
            case "military":
                if (militarySouls < amount) return false;
                militarySouls -= amount;
                break;
            case "spy":
                if (spySouls < amount) return false;
                spySouls -= amount;
                break;
            case "industry":
                if (industrySouls < amount) return false;
                industrySouls -= amount;
                break;
            default:
                Debug.LogWarning("Unknown allocation type.");
                return false;
        }

        availableSouls += amount;
        return true;
    }

    // === Unique Soul Handling ===
    public void AddUniqueSoul(string name)
    {
        if (!uniqueSoulNames.Contains(name))
        {
            uniqueSoulNames.Add(name);
        }
    }

    public bool HasUniqueSoul(string name)
    {
        return uniqueSoulNames.Contains(name);
    }
}
