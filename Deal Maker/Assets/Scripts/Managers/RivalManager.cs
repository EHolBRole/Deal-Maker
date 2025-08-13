using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RelationThresholds
{
    public const int ENEMY_THRESHOLD = -30;
    public const int ALLY_THRESHOLD = 30;
}

public static class ResourceThresholds
{
    public const int LOW_AMOUNT = 100;
    public const int HIGH_AMOUNT = 1000;
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
        }
    }
    public SchemeData ChooseScheme(DemonState executor, DemonState target) // Make a check so Demon couldn't improve Relations with player!
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

        // Filter by category
        var availableSchemes = executor.schemeList
            .Where(s => s.schemeCategory == category)
            .ToList();

        if (availableSchemes.Count == 0)
            return null;

        // Apply personality weights
        var weightedSchemes = new List<(SchemeData scheme, float weight)>();
        foreach (var scheme in availableSchemes)
        {
            float personalityWeight = executor.personality.GetSchemeWeight(scheme);
            float strategicWeight = scheme.CalculateStrategicWeight(executor, target);

            float factionWeight = 1f;
            if (executor.faction.GetRelation(target.faction.factionID) < -30) factionWeight = 1.2f; // more aggressive
            else if (executor.faction.GetRelation(target.faction.factionID) > 30) factionWeight = 0.8f; // more cooperative
            // Combine personality, strategicWeight and factionWeight
            float combinedWeight = personalityWeight * strategicWeight * factionWeight;
            weightedSchemes.Add((scheme, combinedWeight));
        }

        // Weighted random selection
        float totalWeight = weightedSchemes.Sum(ws => ws.weight);
        float randomValue = Random.Range(0f, totalWeight);
        float runningSum = 0f;

        foreach (var ws in weightedSchemes)
        {
            runningSum += ws.weight;
            if (randomValue <= runningSum)
                return ws.scheme;
        }

        return null; // Fallback
    }

    public DemonState ChooseTarget(DemonState executor)
    {
        var candidates = new List<(DemonState target, float weight)>();

        foreach (var d in demons.Concat(new[] { playerController.player }))
        {
            if (d.demonID == executor.demonID)
                continue;

            float relation = executor.GetRelation(d.demonID);
            float weight = 1f;

            if (relation < RelationThresholds.ENEMY_THRESHOLD)
                weight *= executor.personality.GetAggression();    // enemies weighted by aggression
            else if (relation > RelationThresholds.ALLY_THRESHOLD)
                weight *= executor.personality.GetLoyalty();       // allies weighted by loyalty
            else
                weight *= executor.personality.GetCaution();       // neutrals weighted by caution

            if (executor.faction != null && d.faction != null)
            {
                int factionRelation = executor.faction.GetRelation(d.faction.factionID);
                if (factionRelation < -30) weight *= 1.2f; // hostile factions → more attractive target
                else if (factionRelation > 30) weight *= 0.8f; // friendly factions → less attractive
            }
            // Add only targets with positive weight
            if (weight > 0f)
                candidates.Add((d, weight));
        }

        if (candidates.Count == 0)
            return null;

        // Weighted random selection
        float totalWeight = candidates.Sum(c => c.weight);
        float randomValue = Random.Range(0f, totalWeight);
        float runningSum = 0f;

        foreach (var c in candidates)
        {
            runningSum += c.weight;
            if (randomValue <= runningSum)
                return c.target;
        }

        return null;

    }
}
