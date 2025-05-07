using System.Collections.Generic;
using UnityEngine;

public class DealManager : MonoBehaviour
{
    public static DealManager Instance { get; private set; }

    private List<DealData> unlockedDeals = new List<DealData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void UnlockDeal(DealData deal)
    {
        if (!unlockedDeals.Contains(deal))
        {
            unlockedDeals.Add(deal);
        }
    }

    public List<DealData> GetUnlockedDeals()
    {
        return unlockedDeals;
    }

    public void RemoveUnlockedDeal(DealData deal)
    {
        unlockedDeals.Remove(deal);
    }
}
