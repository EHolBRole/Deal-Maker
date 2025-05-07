using UnityEngine;

[CreateAssetMenu(fileName = "New Unlock Deal Action", menuName = "Actions/Unlock Deal Action")]
public class UnlockDealAction : ActionData
{
    public DealData dealToUnlock;

    public override void ExecuteAction()
    {
        Debug.Log("Unlocked new Deal!");
    }
}
