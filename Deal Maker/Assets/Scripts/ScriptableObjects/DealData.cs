using UnityEngine;


[CreateAssetMenu(fileName = "New Deal", menuName = "Deals/Deal Data")]
public class DealData : ScriptableObject
{
    public string dealTitle;
    [TextArea(2, 5)] public string dealDescription;

    public int soulCoinsCost;
    public int influenceCost;
    public int secretsCost;
    public int soulsCost;

    public int soulCoinsReward;
    public int influenceReward;
    public int secretsReward;
    public int soulsReward;

    public bool isWithMortal; // true = mortal, false = demon
}
