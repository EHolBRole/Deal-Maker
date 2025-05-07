using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int soulCoins = 0;
    public int influence = 0;
    public int secrets = 0;

    public void ChangeSoulCoins(int amount) => soulCoins += amount;
    public void ChangeInfluence(int amount) => influence += amount;
    public void ChangeSecrets(int amount) => secrets += amount;
}
