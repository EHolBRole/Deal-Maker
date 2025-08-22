using System.Collections.Generic;
using UnityEngine;

public enum DemonRank { Baron, Marquis, Duke, DeadlySin, King }

[System.Serializable]
public class DemonRelationData
{
    public DemonData otherDemon;
    [Range(-100, 100)]
    public int initialRelation;
}

[System.Serializable]
public class PreferredScheme
{
    public SchemeData scheme;
    [Range(1, 100)]
    public int weight = 10;
}

[CreateAssetMenu(menuName = "Politics/DemonTemplate")]
public class DemonData : ScriptableObject
{
    public string demonName;
    public string DemonID;
    [Header("Politics")]
    public DemonRank rank;
    public FactionData faction;
    public List<SchemeData> schemeList;
    public List<DemonRelationData> startingRelations = new List<DemonRelationData>();
    public List<DemonTraitData> traits = new List<DemonTraitData>();
    [Header("Base resources")]
    public int baseSoulCoins;
    public int baseInfluence;
    public int baseSecrets;
    public int baseSouls;
    [Header("Base stats")]
    public int baseCharisma;
    public int baseLeadership;
}
