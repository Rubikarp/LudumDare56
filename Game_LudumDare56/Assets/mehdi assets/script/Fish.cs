using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishMoovement))]
public class Fish : MonoBehaviour
{
    public FishData fishData;
    private FishMoovement fishMoovement;

    private void Awake()
    {
        fishMoovement = GetComponent<FishMoovement>();
    }

    public void Init(FishZone moveArea)
    {
        fishMoovement.moveArea = moveArea;
    }

    void Update()
    {
        
    }
}

public enum FishSpecies
{
    Pike,
    Carp,
    Trout,
    Salmon,
    Catfish,
}

public class FishData
{
    public FishSpecies specie;
    public int weight;
    public int length;

    public bool isCaught;
    public float caughtTime;
    public int Price => weight * length;

    public FishData(FishSpecies specie, int weight, int length)
    {
        this.specie = specie;
        this.weight = weight;
        this.length = length;

        isCaught = false;
        caughtTime = 0;
    }
}