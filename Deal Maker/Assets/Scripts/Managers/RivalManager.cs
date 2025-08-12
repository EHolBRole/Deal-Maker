using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.UI;
using static UnityEngine.GraphicsBuffer;

public static class RelationThresholds
{
    public const int ENEMY_THRESHOLD = -30;
    public const int ALLY_THRESHOLD = 30;
}


public class RivalManager : MonoBehaviour
{
    public List<DemonData> demonTemplates;
    public List<FactionData> factionTemplates;

    public List<DemonState> demons = new List<DemonState>();
    public List<FactionState> factions = new List<FactionState>();

    public PlayerDemonController playerController;

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
            demon.ModifyRelation(playerController.player.demonID, Random.Range(-6, 5));
            Debug.Log($"{demon.name} relation to player: {demon.GetRelation(playerController.player.demonID)}");
            TakeTurn(demon);

        }
    }
    public void TakeTurn(DemonState demon)
    {
        DemonState target = ChooseTarget(demon);
        SchemeData scheme = ChooseScheme(demon, target);

        if (target != null && scheme != null)
        {
            scheme.Execute(demon, target);
        }
        else
        {
            // Optionally handle no action or fallback here
            Debug.Log($"{demon.name} has no valid political action to perform this turn.");
            Debug.Log(target);
            Debug.Log(scheme);
        }
    }
    public SchemeData ChooseScheme(DemonState executor, DemonState target)
    {
        if (target == null)
            return null;

        float relation = executor.GetRelation(target.demonID);
        SchemeCategory category;

        if (relation < RelationThresholds.ENEMY_THRESHOLD)
            category = SchemeCategory.Hostile;
        else if (relation > RelationThresholds.ALLY_THRESHOLD)
            category = SchemeCategory.Friendly;
        else
            category = SchemeCategory.Neutral;

        var preferredByCategory = executor.preferredSchemes
        .Where(ps => ps.scheme.schemeCategory == category)
        .ToList();

        if (preferredByCategory.Count > 0)
        {
            return GetWeightedRandomPreferredScheme(preferredByCategory);
        }
        else 
        { 
            var availableSchemes = executor.schemeList
                .Where(s => s.schemeCategory == category)
                .ToList();

            if (availableSchemes.Count == 0)
                return null;

            return availableSchemes[Random.Range(0, availableSchemes.Count)];

        }

    }
    private SchemeData GetWeightedRandomPreferredScheme(List<PreferredScheme> preferredSchemes)
    {
        int totalWeight = preferredSchemes.Sum(ps => ps.weight);
        int randomValue = Random.Range(0, totalWeight);
        int runningSum = 0;

        foreach (var ps in preferredSchemes)
        {
            runningSum += ps.weight;
            if (randomValue < runningSum)
                return ps.scheme;
        }

        return null;
    }
    public DemonState ChooseTarget(DemonState executor)
    {
        var enemies = demons
            .Where(d => executor.GetRelation(d.demonID) < RelationThresholds.ENEMY_THRESHOLD &&
            d.demonID != executor.demonID)
            .ToList();

        if (executor.GetRelation(playerController.player.demonID) < RelationThresholds.ENEMY_THRESHOLD)
            enemies.Add(playerController.player);

        if (enemies.Count > 0)
        {
            return enemies[Random.Range(0, enemies.Count)];
        }

        var allies = demons
            .Where(d => executor.GetRelation(d.demonID) > RelationThresholds.ALLY_THRESHOLD &&
            d.demonID != executor.demonID)
            .ToList();

        if (executor.GetRelation(playerController.player.demonID) > RelationThresholds.ALLY_THRESHOLD)
            allies.Add(playerController.player);

        if (allies.Count > 0)
        {
            return allies[Random.Range(0, allies.Count)];
        }

        var neutrals = demons
            .Where(d => executor.GetRelation(d.demonID) >= RelationThresholds.ENEMY_THRESHOLD && 
            executor.GetRelation(d.demonID) <= RelationThresholds.ALLY_THRESHOLD && 
            d.demonID != executor.demonID)
            .ToList();

        if (executor.GetRelation(playerController.player.demonID) >= RelationThresholds.ENEMY_THRESHOLD &&
            executor.GetRelation(playerController.player.demonID) <= RelationThresholds.ALLY_THRESHOLD)
            neutrals.Add(playerController.player);

        if (neutrals.Count > 0)
        {
            return neutrals[Random.Range(0, neutrals.Count)];
        }

        return null; // No valid target

    }
}
