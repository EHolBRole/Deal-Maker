using System;
using System.Collections.Generic;
using UnityEngine;

public class DemonState
{
    public string name;
    public string demonID;
    //Politics
    public DemonRank rank;
    public FactionState faction;
    public DemonPersonality personality;
    public List<SchemeData> schemeList;
    // Base resources
    public int soulCoins;
    public int influence;
    public int secrets;
    public int souls;
    // Base Stats
    public int charisma;
    public int leadership;

    [SerializeField]
    private Dictionary<string, int> relations = new Dictionary<string, int>();

    public DemonState(DemonData template)
    {
        name = template.demonName;
        demonID = template.DemonID;

        
        rank = template.rank;
        faction = new FactionState(template.faction);
        personality = new DemonPersonality();
        schemeList = template.schemeList;

        foreach (var r in template.startingRelations)
        {
            SetRelation(r.otherDemon.DemonID, r.initialRelation);
        }
        

        foreach (var trait in template.traits)
        {
            personality.traits.Add(trait);
        }


        influence = template.baseInfluence;
        soulCoins = template.baseSoulCoins;
        secrets = template.baseSecrets;
        souls = template.baseSouls;

        charisma = template.baseCharisma;
        leadership = template.baseLeadership;


    }

    public float GetPowerScore()
    {
        // Combine resources + stats into a single score
        float score = influence + secrets + souls;
        score += charisma + leadership;
        return score;
    }

    public int GetResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.SoulCoins:
                return this.soulCoins;
            case ResourceType.Influence:
                return this.influence;
            case ResourceType.Secrets:
                return this.secrets;
            case ResourceType.Souls:
                return this.souls;
            default:
                return 0;
        }
    }

    public void ModifyResource(ResourceType type, int value)
    {
        switch (type)
        {
            case ResourceType.SoulCoins:
                this.soulCoins += value;
                break;
            case ResourceType.Influence:
                this.influence += value;
                break;
            case ResourceType.Secrets:
                this.secrets += value;
                break;
            case ResourceType.Souls:
                this.souls += value;
                break;
        }
    }

    public void SetRelation(string targetID, int value)
    {
        relations[targetID] = Mathf.Clamp(value, -100, 100);
    }

    public int GetRelation(string targetID)
    {
        if (relations.TryGetValue(targetID, out int value))
            return value;
        return 0; // default neutral
    }

    public void ModifyRelation(string targetID, int delta)
    {
        int current = GetRelation(targetID);
        SetRelation(targetID, current + delta);
    }

    public Dictionary<string, int> GetAllRelations()
    {
        return relations;
    }
}
