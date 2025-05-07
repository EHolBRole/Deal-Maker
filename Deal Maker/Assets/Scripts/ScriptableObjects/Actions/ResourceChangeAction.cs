using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Action", menuName = "Actions/Resource Action")]
public class ResourceChangeAction : ActionData
{
    public int soulCoinChange;

    public int influenceChange;

    public int secretsChange;

    public override void ExecuteAction()
    {
        Debug.Log("Changed players resources!");

        ResourceManager manager = FindObjectOfType<ResourceManager>();
        
        manager.ChangeSoulCoins(soulCoinChange);
        manager.ChangeInfluence(influenceChange);
        manager.ChangeSecrets(secretsChange);

    }
}
