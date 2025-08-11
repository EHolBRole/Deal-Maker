using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int currentWeek = 1;
    public int maxMajorActions;
    public int maxMinorActions;

    private int majorActionsUsed = 0;
    private int minorActionsUsed = 0;

    public int GetMajorActionsUsed() => majorActionsUsed;
    public int GetMinorActionsUsed() => minorActionsUsed;

    public void UseMajorAction()
    {
        if (majorActionsUsed < maxMajorActions)
        {
            majorActionsUsed++;
            Debug.Log("Used a Major Action. Remaining: " + (maxMajorActions - majorActionsUsed));
        }
        else
        {
            Debug.Log("No Major Actions left this week.");
        }
    }

    public void UseMinorAction()
    {
        if (minorActionsUsed < maxMinorActions)
        {
            minorActionsUsed++;
            Debug.Log("Used a Minor Action. Remaining: " + (maxMinorActions - minorActionsUsed));
        }
        else
        {
            Debug.Log("No Minor Actions left this week.");
        }
    }

    public void EndTurn()
    {
        Debug.Log("Week " + currentWeek + " ended.");
        Debug.Log("Major Actions used: " + majorActionsUsed + " / " + maxMajorActions);
        Debug.Log("Minor Actions used: " + minorActionsUsed + " / " + maxMinorActions);

        currentWeek++;

        FindObjectOfType<EventManager>().TriggerRandomEvent();
        FindObjectOfType<RivalManager>().RunWeeklyTick();

        majorActionsUsed = 0;
        minorActionsUsed = 0;

        Debug.Log("Week " + currentWeek + " begins!");
    }

    public bool CanUseMajorAction()
    {
        return majorActionsUsed < maxMajorActions;
    }

    public bool CanUseMinorAction()
    {
        return minorActionsUsed < maxMinorActions;
    }
}
