using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class DemonPersonality
{
    public List<DemonTraitData> traits = new List<DemonTraitData>();

    public float GetSchemeWeight(SchemeData scheme)
    {
        float weight = 1f;
        foreach (var trait in traits)
        {
            var pref = trait.schemePreferences.FirstOrDefault(p => p.scheme == scheme);
            if (!pref.Equals(default(SchemePreference)))
                weight *= pref.weight;
        }
        return weight;
    }

    public float GetAggression()
    {
        return traits.Aggregate(1f, (current, trait) => current * trait.aggressionMultiplier);
    }

    public float GetCaution()
    {
        return traits.Aggregate(1f, (current, trait) => current * trait.cautionMultiplier);
    }

    public float GetLoyalty()
    {
        return traits.Aggregate(1f, (current, trait) => current * trait.loyaltyMultiplier);
    }
}
