using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DealMenuManager : MonoBehaviour
{
    public GameObject dealMenuPanel;
    public GameObject dealButtonPrefab; 

    public Transform dealListParent; 

    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
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
            texts[1].text = $"Cost: {deal.soulCost} Souls, {deal.influenceCost} Influence, {deal.secretsCost} Secrets\n" +
                            $"Gain: +{deal.soulReward} Souls, +{deal.influenceReward} Influence, +{deal.secretsReward} Secrets";

            Button button = btn.GetComponent<Button>();
            button.onClick.AddListener(() => AcceptDeal(deal));
        }
    }

    void AcceptDeal(DealData deal)
    {
        
        if (resourceManager.souls >= deal.soulCost &&
            resourceManager.influence >= deal.influenceCost &&
            resourceManager.secrets >= deal.secretsCost)
        {
            Debug.Log("Made deal: " + deal.dealTitle);
            resourceManager.ChangeSouls(-deal.soulCost);
            resourceManager.ChangeInfluence(-deal.influenceCost);
            resourceManager.ChangeSecrets(-deal.secretsCost);

            resourceManager.ChangeSouls(deal.soulReward);
            resourceManager.ChangeInfluence(deal.influenceReward);
            resourceManager.ChangeSecrets(deal.secretsReward);

            DealManager.Instance.RemoveUnlockedDeal(deal);
        }
        else
        {
            Debug.Log("Couldn't make a deal: " + deal.dealTitle);
        }
        
        PopulateDeals();
    }
}
