using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DealMenuManager : MonoBehaviour
{
    public GameObject dealMenuPanel;
    public GameObject dealButtonPrefab; 

    public Transform dealListParent; 
    public PlayerDemonController playerController;
    private ResourceManager resourceManager;
    private SoulsManager soulsManager;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        soulsManager = FindObjectOfType<SoulsManager>();    
        dealMenuPanel.SetActive(false); 
    }

    public void ToggleDealMenu()
    {
        dealMenuPanel.SetActive(!dealMenuPanel.activeSelf);

        if (dealMenuPanel.activeSelf)
        {
            PopulateDeals();
        }
    }

    void PopulateDeals()
    {
        
        foreach (Transform child in dealListParent)
        {
            Destroy(child.gameObject);
        }

        var unlockedDeals = DealManager.Instance.GetUnlockedDeals();

        foreach (DealData deal in unlockedDeals)
        {
            GameObject btn = Instantiate(dealButtonPrefab, dealListParent);
            TMP_Text[] texts = btn.GetComponentsInChildren<TMP_Text>();

            
            texts[0].text = deal.dealTitle;
            texts[1].text = $"Cost: {deal.soulsReward} Souls,{deal.soulCoinsCost} SoulCoins, {deal.influenceCost} Influence, {deal.secretsCost} Secrets\n" +
                            $"Gain: +{deal.soulsReward} Souls, +{deal.soulCoinsReward} SoulCoins, +{deal.influenceReward} Influence, +{deal.secretsReward} Secrets";

            Button button = btn.GetComponent<Button>();
            button.onClick.AddListener(() => AcceptDeal(deal));
        }
    }

    void AcceptDeal(DealData deal)
    {
        Debug.Log(deal.soulCoinsCost);
        Debug.Log(soulsManager.availableSouls);
        Debug.Log(playerController.player.soulCoins);
        if (playerController.player.soulCoins >= deal.soulCoinsCost &&
            playerController.player.influence >= deal.influenceCost &&
            playerController.player.secrets >= deal.secretsCost &&
            soulsManager.availableSouls >= deal.soulsCost)
        {
            Debug.Log("Made deal: " + deal.dealTitle);
            resourceManager.ChangeSoulCoins(-deal.soulCoinsCost);
            resourceManager.ChangeInfluence(-deal.influenceCost);
            resourceManager.ChangeSecrets(-deal.secretsCost);
            soulsManager.RemoveSouls(deal.soulsCost);

            resourceManager.ChangeSoulCoins(deal.soulCoinsReward);
            resourceManager.ChangeInfluence(deal.influenceReward);
            resourceManager.ChangeSecrets(deal.secretsReward);
            soulsManager.AddSouls(deal.soulsReward);

            DealManager.Instance.RemoveUnlockedDeal(deal);
        }
        else
        {
            Debug.Log("Couldn't make a deal: " + deal.dealTitle);
        }
        
        PopulateDeals();
    }
}
