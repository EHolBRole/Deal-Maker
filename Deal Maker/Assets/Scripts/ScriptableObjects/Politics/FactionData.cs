using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FactionRelationData
{
    public FactionData otherFaction;
    [Range(-100, 100)]
    public int initialRelation;
}


[CreateAssetMenu(menuName = "Politics/FactionTemplate")]
public class FactionData : ScriptableObject
{
    public string factionName; 
    public string factionID; 
    public List<FactionRelationData> startingRelations = new List<FactionRelationData>();

}
