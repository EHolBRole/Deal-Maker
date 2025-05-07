using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Action", menuName = "Actions/Resource Change Action")]
public class ResourceChangeAction : ActionData
{
    public int soulCoinChange;

    public int influenceChange;

    public int secretsChange;

    public override void ExecuteAction()
    {
        Debug.Log("Changed players resources!");

        ResourceManager manager = FindObjectOfType<ResourceManager>();
        
        manager.ChangeSouls(soulCoinChange);
        manager.ChangeInfluence(influenceChange);
        manager.ChangeSecrets(secretsChange);

    }
}
