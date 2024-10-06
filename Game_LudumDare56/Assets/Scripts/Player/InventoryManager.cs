using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    private PlayerData playerData;
    public List<FishItem> fishCaught = new List<FishItem>();

    protected override void Awake()
    {
        base.Awake();

        playerData = PlayerData.Instance;
        PlayerNet.onFishCaught += AddFish;
    }

    public void AddFish(FishItem fishItem)
    {
        if(fishCaught.Count >= playerData.defaultCapacity)
        {
            Debug.Log("Inventory is full");
            return;
        }

        fishCaught.Add(fishItem);
        UpdateHUD();
    }
    public void RemoveFish(FishItem fishItem)
    {
        if(!fishCaught.Contains(fishItem))
        {
            Debug.LogWarning("Fish not found in inventory");
            return;
        }
        fishCaught.Remove(fishItem);
        UpdateHUD();
    }

    private void UpdateHUD()
    {

    }
}

[System.Serializable]
public class FishItem
{
    public FishData data;
    public float caughtTime;

    public int Price
    {
        get
        {
            var freshness = Mathf.Clamp01(1 - (Time.time - caughtTime) / data.defaultFreshness);
            return data.basePrice;
        }
    }
    public FishItem(FishData data)
    {
        this.data = data;
        caughtTime = Time.time;
    }
}
