using UnityEngine;


public enum ResourceType
{
    Influence, SoulCoins, Secrets, Souls
}

public class ResourceManager : MonoBehaviour
{
    public PlayerDemonController playerController;

    public void ChangeSoulCoins(int amount) => playerController.player.ModifyResource(ResourceType.SoulCoins, amount);
    public void ChangeInfluence(int amount) => playerController.player.ModifyResource(ResourceType.Influence, amount);
    public void ChangeSecrets(int amount) => playerController.player.ModifyResource(ResourceType.Secrets, amount);
}
