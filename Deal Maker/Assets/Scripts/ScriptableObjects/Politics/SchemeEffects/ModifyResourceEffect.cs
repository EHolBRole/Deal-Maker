using UnityEngine;

[CreateAssetMenu(menuName = "Politics/Scheme Effects/Modify Resource")]
public class ModifyResourceEffect : SchemeEffect
{
    public ResourceType resourceType;

    public int amount;

    public bool isOnTarget;


    public override void Apply(DemonState executor, DemonState target)
    {
        if (executor != null)
        {
            if(isOnTarget)
                target.ModifyResource(resourceType, amount);
            else
                executor.ModifyResource(resourceType, amount);
        }
    }
}
