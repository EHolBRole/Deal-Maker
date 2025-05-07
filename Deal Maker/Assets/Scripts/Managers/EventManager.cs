using System.Resources;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public EventData[] possibleEvents;

    public GameObject eventPanel;
    public TMP_Text eventTitleText;
    public TMP_Text eventDescText;
    public Button[] choiceButtons;

    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void TriggerRandomEvent()
    {
        if (possibleEvents.Length == 0) return;

        EventData e = possibleEvents[Random.Range(0, possibleEvents.Length)];
        DisplayEvent(e);
    }

    public void DisplayEvent(EventData e)
    {
        eventPanel.SetActive(true);
        eventTitleText.text = e.eventTitle;
        eventDescText.text = e.eventDescription;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < e.choices.Length)
            {
                EventChoice choice = e.choices[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choice.choiceText;
                choiceButtons[i].gameObject.SetActive(true);

                int index = i; // closure fix
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    ApplyChoice(choice);
                    eventPanel.SetActive(false);
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void ApplyChoice(EventChoice choice)
    {
        resourceManager.ChangeSoulCoins(choice.soulChange);
        resourceManager.ChangeInfluence(choice.influenceChange);
        resourceManager.ChangeSecrets(choice.secretChange);
        Debug.Log("🔮 Event choice applied.");
    }
}
