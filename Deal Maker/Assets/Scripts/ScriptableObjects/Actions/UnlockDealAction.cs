using UnityEngine;

[CreateAssetMenu(fileName = "New Deal Action", menuName = "Actions/Deal Action")]
public class UnlockDealAction : ActionData
{
    public DealData dealToUnlock;

    public override void ExecuteAction()
    {
        DealManager.Instance.UnlockDeal(dealToUnlock);
        Debug.Log("Unlocked deal: " + dealToUnlock.dealTitle);
    }
}
