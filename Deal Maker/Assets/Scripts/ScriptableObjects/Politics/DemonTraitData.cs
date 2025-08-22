using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDemonTrait", menuName = "Politics/Demon Trait")]
public class DemonTraitData : ScriptableObject
{
    public string traitName;
    [TextArea] public string description;

    [Header("Scheme Preferences")]
    public List<SchemePreference> schemePreferences = new List<SchemePreference>();

    [Header("Behavior Modifiers")]
    [Range(0f, 2f)] public float aggressionMultiplier = 1f;
    [Range(0f, 2f)] public float cautionMultiplier = 1f;
    [Range(0f, 2f)] public float loyaltyMultiplier = 1f;
}

[System.Serializable]
public struct SchemePreference
{
    public SchemeData scheme;
    [Range(-2f, 2f)] public float weight; // 1.0 = neutral, >1 = prefers, <1 = avoids
}