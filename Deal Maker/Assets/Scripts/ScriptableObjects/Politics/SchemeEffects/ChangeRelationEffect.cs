using UnityEngine;

[CreateAssetMenu(menuName = "Politics/Scheme Effects/Change Relation")]
public class ChangeRelationEffect : SchemeEffect
{
    [Range(-100, 100)]public int amount;

    public override void Apply(DemonState executor, DemonState target)
    {
        if (executor != null && target != null)
        {
            target.ModifyRelation(executor.demonID, amount);
        }
    }
}