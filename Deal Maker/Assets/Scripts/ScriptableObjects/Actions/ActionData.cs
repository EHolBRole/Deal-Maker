using UnityEngine;


[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Action")]
public class ActionData : ScriptableObject
{
    public string actionName;
    [TextArea] public string description;
    public bool isMajorAction;

    public virtual void ExecuteAction()
    {
        Debug.Log($"Executing Action: {actionName}");
        // Placeholder — logic will go here
    }
}
