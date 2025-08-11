using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public PlayerDemonController playerController;

    public TextMeshProUGUI soulCoinsText;
    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI secretsText;

    void Update()
    {
        soulCoinsText.text = $"SoulCoins: {playerController.player.soulCoins}";
        influenceText.text = $"Influence: {playerController.player.influence}";
        secretsText.text = $"Secrets: {playerController.player.secrets}";
    }
}
