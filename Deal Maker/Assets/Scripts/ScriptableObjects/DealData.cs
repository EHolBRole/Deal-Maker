using UnityEngine;


[CreateAssetMenu(fileName = "New Deal", menuName = "Deal Maker/Deal Data")]
public class DealData : ScriptableObject
{
    public string dealTitle;
    [TextArea(2, 5)] public string dealDescription;

    public int soulCost;
    public int influenceCost;
    public int secretsCost;

    public int soulReward;
    public int influenceReward;
    public int secretsReward;

    public bool isWithMortal; // true = mortal, false = demon
}
