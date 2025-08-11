public class DemonState
{
    public string name;
    //Politics
    public string factionName;
    public DemonRank rank;
    // Base resources
    public int soulCoins;
    public int influence;
    public int secrets;
    public int souls;
    // Base Stats
    public int charisma;
    public int leadership;

    public int relationToPlayer; // -100 to +100

    public DemonState(DemonTemplateSO template)
    {
        name = template.demonName;
        rank = template.rank;
        factionName = template.factionName;

        influence = template.baseInfluence;
        soulCoins = template.baseSoulCoins;
        secrets = template.baseSecrets;
        souls = template.baseSouls;

        charisma = template.baseCharisma;
        leadership = template.baseLeadership;

        relationToPlayer = 0;
    }
}
