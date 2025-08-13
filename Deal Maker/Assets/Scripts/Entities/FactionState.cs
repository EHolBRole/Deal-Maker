using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FactionState
{
    public string factionName;
    public string factionID;

    [SerializeField]
    public Dictionary<string, int> relations = new Dictionary<string, int>();

    public FactionState(FactionData template)
    {
        factionName = template.factionName;
        factionID = template.factionID;
        foreach (var r in template.startingRelations)
        {
            SetRelation(r.otherFaction.factionID, r.initialRelation);
        }
    }

    public void SetRelation(string factionID, int value)
    {
        relations[factionID] = value;
    }

    public int GetRelation(string factionID)
    {
        if (relations.TryGetValue(factionID, out int value))
            return value;
        return 0; // default neutral
    }
}
