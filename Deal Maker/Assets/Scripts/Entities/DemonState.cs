using System;
using System.Collections.Generic;
using UnityEngine;

public class DemonState
{
    public string name;
    public string demonID;
    //Politics
    public string factionName;
    public DemonRank rank;
    public List<SchemeData> schemeList;
    public List<PreferredScheme> preferredSchemes;
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

        factionName = template.factionName;
        rank = template.rank;
        schemeList = template.schemeList;


        influence = template.baseInfluence;
        soulCoins = template.baseSoulCoins;
        secrets = template.baseSecrets;
        souls = template.baseSouls;

        charisma = template.baseCharisma;
        leadership = template.baseLeadership;

        foreach(var r in template.startingRelations)
        {
            SetRelation(r.otherDemon.DemonID, r.initialRelation);
        }

        preferredSchemes = new List<PreferredScheme>();
        foreach (var pref in template.preferredSchemes)
        {
            // THINK: Create new PreferredScheme instances if needed
            preferredSchemes.Add(pref);
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
