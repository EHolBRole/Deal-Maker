using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Events/EventData")]
public class EventData : ScriptableObject
{
    public string eventTitle;
    [TextArea] public string eventDescription;

    public EventChoice[] choices;
}


[System.Serializable]
public class EventChoice
{
    public string choiceText;
    public int soulChange;
    public int influenceChange;
    public int secretChange;
}
