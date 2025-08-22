using System.Collections.Generic;
using UnityEngine;


public enum SchemeCategory // TODO: Put in another file.
{
    Hostile,  // sabotage, assassinate
    Friendly, // help, share
    Neutral   // spy, propose deals
}

[CreateAssetMenu(fileName = "New Scheme", menuName = "Politics/Scheme")]
public class SchemeData : ScriptableObject
{
    public string schemeName;
    public int costInfluence;
    public int costSecrets;
    [Range(0, 100)] public int baseSuccessChance;

    [TextArea] public string description;

    [Header("Ex.name + text below + targ.name")]
    [TextArea] public string debugLogSuccess;
    [TextArea] public string debugLogFailure;

    public SchemeCategory schemeCategory;
    
    public List<SchemeEffect> successEffects;
    public List<SchemeEffect> failureEffects;

    public float CalculateStrategicWeight(DemonState executor, DemonState target)
    {
        if (target == null)
            return 0f;

        float weight = 1f;

        float executorPower = executor.GetPowerScore();
        float targetPower = target.GetPowerScore();
        float powerRatio = targetPower / Mathf.Max(1f, executorPower);

        // Base category modifier
        switch (schemeCategory)
        {
            case SchemeCategory.Hostile:
                weight *= 1f + (1f - Mathf.Clamp01(powerRatio)); // weaker target preferred
                break;
            case SchemeCategory.Friendly:
                weight *= 1f + Mathf.Clamp01(powerRatio); // stronger target preferred
                break;
            case SchemeCategory.Neutral:
                weight *= 1f; // default neutral
                break;
        }


        // Calculate expected effect impact
        float expectedValue = 0f;

        // Success effects
        foreach (var effect in successEffects)
        {
            float effectWeight = EvaluateEffectWeight(executor, target, effect);
            expectedValue += effectWeight * (baseSuccessChance / 100f);
        }

        // Failure effects
        foreach (var effect in failureEffects)
        {
            float effectWeight = EvaluateEffectWeight(executor, target, effect);
            expectedValue += effectWeight * (1f - baseSuccessChance / 100f);
        }

        // Combine with category & power weight
        weight *= Mathf.Max(0.1f, expectedValue); // ensure weight never drops below minimal threshold

        return weight;
    }

    // Separate method to calculate effect weight for strategy
    private float EvaluateEffectWeight(DemonState executor, DemonState target, SchemeEffect effect)
    {
        float weight = 1f;

        if (effect is ModifyResourceEffect resEffect)
        {

            float currentAmount = executor.GetResource(resEffect.resourceType);

            if (currentAmount <= ResourceThresholds.LOW_AMOUNT)
                weight *= 1.5f;
            else if (currentAmount >= ResourceThresholds.HIGH_AMOUNT)
                weight *= 0.7f;

            weight *= Mathf.Max(0.1f, resEffect.amount / 10f); // scale by magnitude
        }
        else if (effect is ChangeRelationEffect relEffect)
        {
            if (relEffect.amount > 0)
                weight *= 1f + Mathf.Clamp01(target.GetPowerScore() / Mathf.Max(1f, executor.GetPowerScore()));
            else if (relEffect.amount < 0)
                weight *= 0.3f; // avoid schemes that hurt relations unnecessarily
        }

        return weight;
    }

    public void Execute(DemonState executor, DemonState target)
    {
        if (executor.influence < costInfluence || executor.secrets < costSecrets)
        {
            Debug.Log($"{executor.name} cannot afford {schemeName}!");
            return;
        }

        executor.influence -= costInfluence;
        executor.secrets -= costSecrets;

        bool success = Random.Range(0, 100) < baseSuccessChance;

        var effects = success ? successEffects : failureEffects;

        foreach (var effect in effects)
        {
            effect.Apply(executor, target);
        }

        if (success)
            Debug.Log($"{executor.name} " + debugLogSuccess + $" {target.name}");
        else
            Debug.Log($"{executor.name} " + debugLogFailure + $" {target.name}");

    }
}
