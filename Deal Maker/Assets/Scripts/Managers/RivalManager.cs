using System.Collections.Generic;
using UnityEngine;

public class RivalManager : MonoBehaviour
{
    public List<DemonTemplateSO> demonTemplates;
    public List<FactionTemplateSO> factionTemplates;

    public List<DemonState> demons = new List<DemonState>();
    public List<FactionState> factions = new List<FactionState>();



    void Start()
    {
        InitializeGameData();
    }

    void InitializeGameData()
    {
        factions.Clear();
        foreach (var f in factionTemplates)
            factions.Add(new FactionState(f));

        demons.Clear();
        foreach (var d in demonTemplates)
            demons.Add(new DemonState(d));

    }

    public void RunWeeklyTick()
    {
        foreach (var demon in demons)
        {
            demon.relationToPlayer += Random.Range(-5, 6);
            demon.relationToPlayer = Mathf.Clamp(demon.relationToPlayer, -100, 100); 
            Debug.Log($"{demon.name} relation to player: {demon.relationToPlayer}");

        }
    }
}
