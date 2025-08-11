using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public PlayerDemonController playerController;

    public void ChangeSoulCoins(int amount) => playerController.player.soulCoins += amount;
    public void ChangeInfluence(int amount) => playerController.player.influence += amount;
    public void ChangeSecrets(int amount) => playerController.player.secrets += amount;
}
