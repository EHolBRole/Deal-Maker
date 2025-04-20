using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public ResourceManager resourceManager;

    public TextMeshProUGUI soulsText;
    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI secretsText;

    void Update()
    {
        soulsText.text = $"Souls: {resourceManager.souls}";
        influenceText.text = $"Influence: {resourceManager.influence}";
        secretsText.text = $"Secrets: {resourceManager.secrets}";
    }
}
