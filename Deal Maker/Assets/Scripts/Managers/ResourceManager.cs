using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int souls = 0;
    public int influence = 0;
    public int secrets = 0;

    public void ChangeSouls(int amount) => souls += amount;
    public void ChangeInfluence(int amount) => influence += amount;
    public void ChangeSecrets(int amount) => secrets += amount;
}
