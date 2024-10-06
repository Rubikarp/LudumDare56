using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerInventory : SingletonMonoBehaviour<PlayerInventory>
{
    private PlayerData playerData;
    public List<FishItem> fishCaught = new List<FishItem>();

    public List<Image> background = new List<Image>();
    public List<InventoryCase> inventoryCase = new List<InventoryCase>();

    protected override void Awake()
    {
        base.Awake();

        playerData = PlayerData.Instance;
        PlayerNet.onFishCaught += AddFish;
    }

    public void AddFish(FishItem fishItem)
    {
        if(fishCaught.Count >= playerData.CurrentTank)
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

    private void Update()
    {
        UpdateHUD();
    }
    public void UpdateHUD()
    {
        for (int i = 0; i < inventoryCase.Count; i++)
        {
            if(i < fishCaught.Count)
            {
                inventoryCase[i].gameObject.SetActive(true);
                inventoryCase[i].SetFishItem(fishCaught[i]);
            }
            else
            {
                inventoryCase[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < background.Count; i++)
        {
            if (i < playerData.CurrentTank)
            {
                background[i].gameObject.SetActive(true);
            }
            else
            {
                background[i].gameObject.SetActive(false);
            }
        }
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
