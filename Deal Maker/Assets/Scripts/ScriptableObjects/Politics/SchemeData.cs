using UnityEngine;


public enum SchemeType { Spy, Sabotage, Blackmail }
public enum SchemeCategory
{
    Hostile,  // sabotage, assassinate
    Friendly, // help, share
    Neutral   // spy, propose deals
}

[CreateAssetMenu(fileName = "New Scheme", menuName = "Politics/Scheme")]
public class SchemeData : ScriptableObject
{
    public string schemeName;
    public SchemeType type;
    public int costInfluence;
    public int costSecrets;
    [Range(0, 100)] public int baseSuccessChance;
    [TextArea] public string description;

    public SchemeCategory schemeCategory;

    public void Execute(DemonState executor, DemonState target)
    {
        if (executor.influence < costInfluence)
        {
            Debug.Log($"{executor.name} lacks influence to perform {schemeName}!");
            return;
        }
        else if (executor.secrets < costSecrets)
        {
            Debug.Log($"{executor.name} lacks secrets to perform {schemeName}!");
            return;
        }

        executor.influence -= costInfluence;
        executor.secrets -= costSecrets;

        int roll = Random.Range(0, 100);
        bool success = roll < baseSuccessChance;

        if (success) ApplySuccess(executor, target);
        else ApplyFailure(executor, target);
    }

    private void ApplySuccess(DemonState executor, DemonState target)
    {
        switch (type)
        {
            case SchemeType.Spy:
                Debug.Log($"{executor.name} successfully spied on {target.name}!");
                executor.secrets += 5;
                break;
            case SchemeType.Sabotage:
                Debug.Log($"{executor.name} sabotaged {target.name}, lowering influence!");
                target.influence -= 5;
                target.ModifyRelation(executor.demonID, -20);
                break;
            case SchemeType.Blackmail:
                Debug.Log($"{executor.name} blackmailed {target.name}, forcing alliance!");
                target.ModifyRelation(executor.demonID, 15);
                break;
        }
    }

    private void ApplyFailure(DemonState executor, DemonState target)
    {
        Debug.Log($"{executor.name} failed to execute {schemeName} against {target.name}!");
        executor.ModifyRelation(target.demonID, -5);
    }
}
