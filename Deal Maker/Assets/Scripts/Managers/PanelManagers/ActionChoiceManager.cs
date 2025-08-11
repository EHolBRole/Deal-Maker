using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionChoiceManager : MonoBehaviour
{
    public GameObject panel;
    public Transform actionListParent;
    public GameObject actionButtonPrefab;

    public List<ActionData> majorActions;
    public List<ActionData> minorActions;

    private bool isMajor;

    public void ShowActions(bool isMajorAction)
    {
        isMajor = isMajorAction;

        // Check with TurnManager if we’re allowed to show
        if (isMajor && !FindObjectOfType<TurnManager>().CanUseMajorAction()) return;
        if (!isMajor && !FindObjectOfType<TurnManager>().CanUseMinorAction()) return;

        panel.SetActive(true);
        ClearList();

        List<ActionData> actionsToShow = isMajor ? majorActions : minorActions;

        foreach (var action in actionsToShow)
        {
            GameObject buttonGO = Instantiate(actionButtonPrefab, actionListParent);
            buttonGO.GetComponentInChildren<TMP_Text>().text = action.actionName;

            buttonGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                action.ExecuteAction();

                if (isMajor) FindObjectOfType<TurnManager>().UseMajorAction();
                else FindObjectOfType<TurnManager>().UseMinorAction();

                panel.SetActive(false);
            });
        }
    }

    public void ClosePanel() => panel.SetActive(false);

    private void ClearList()
    {
        foreach (Transform child in actionListParent)
        {
            Destroy(child.gameObject);
        }
    }
}
