using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public ResourceManager resourceManager;

    public TextMeshProUGUI soulCoinsText;
    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI secretsText;

    void Update()
    {
        soulCoinsText.text = $"SoulCoins: {resourceManager.soulCoins}";
        influenceText.text = $"Influence: {resourceManager.influence}";
        secretsText.text = $"Secrets: {resourceManager.secrets}";
    }
}
