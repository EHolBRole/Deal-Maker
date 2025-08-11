using UnityEngine;

public enum DemonRank { Baron, Marquis, Duke, DeadlySin, King }

[CreateAssetMenu(menuName = "Template/DemonTemplate")]
public class DemonTemplateSO : ScriptableObject
{
    public string demonName;
    //Politics
    public string factionName;
    public DemonRank rank;
    // Base resources
    public int baseSoulCoins;
    public int baseInfluence;
    public int baseSecrets;
    public int baseSouls;
    // Base Stats
    public int baseCharisma;
    public int baseLeadership;
}
