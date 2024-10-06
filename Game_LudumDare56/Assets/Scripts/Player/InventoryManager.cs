using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class InventoryManager : MonoBehaviour
{
    private PlayerData playerData;
    public List<FishItem> fishCaught = new List<FishItem>();

    private void Awake()
    {
        playerData = PlayerData.Instance;
        PlayerNet.onFishCaught += AddFish;
    }

    private void AddFish(FishItem fishItem)
    {
        if(fishCaught.Count >= playerData.defaultCapacity)
        {
            Debug.Log("Inventory is full");
            return;
        }

        fishCaught.Add(fishItem);
        UpdateHUD();
    }

    private void Update()
    {

    }

    public void UpdateHUD()
    {

    }
}

[System.Serializable]
public class FishItem
{
    public FishData data;
    public readonly int weight;
    public readonly int length;

    public float caughtTime;

    [ShowNativeProperty]
    public int Price => Mathf.CeilToInt(weight * length * .1f);

    public FishItem(FishData data)
    {
        this.data = data;
        this.weight = Random.Range(data.minMaxWeight.x, data.minMaxWeight.y);
        this.length = Random.Range(data.minMaxSize.x, data.minMaxSize.y);

        caughtTime = Time.time;
    }
}
