using UnityEngine;

[CreateAssetMenu(fileName = "New Event Action", menuName = "Actions/Event Action")]
public class EventAction : ActionData
{
    public EventData actionEvent;

    public override void ExecuteAction()
    {
        EventManager manager = FindObjectOfType<EventManager>();

        manager.DisplayEvent(actionEvent);

        Debug.Log("Executed Event Action: " + this.name);
    }
}
