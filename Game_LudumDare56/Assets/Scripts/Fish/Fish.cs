using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishMoovement))]
[RequireComponent(typeof(SpriteRenderer))]
public class Fish : MonoBehaviour
{
    public FishData fishData;
    private FishMoovement fishMoovement;
    private SpriteRenderer spriteRenderer;

    public FishItem CatchFish() 
    { 
        var fishItem = new FishItem(fishData);
        Destroy(gameObject, 0.1f);
        return fishItem; 
    }

    private void Awake()
    {
        fishMoovement = GetComponent<FishMoovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(FishZone moveArea, FishData data)
    {
        fishData = data;
        fishMoovement.moveArea = moveArea;
        spriteRenderer.sprite = fishData.sprite;
    }

    void Update()
    {
        
    }
}

public enum FishSpecies
{
    CraftFish,
    PacFish,
    OrigamiFish,
    IlluminatiFish,
    ChadFish,
}
