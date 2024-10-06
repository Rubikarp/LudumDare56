using UnityEngine;
using NaughtyAttributes;

public class PlayerData : SingletonMonoBehaviour<PlayerData>
{
    public int money = 0;

    [HorizontalLine]
    public int defaultCapacity = 8;
    public float defaultSpeed = 5f;

    public Netdata CurrentNet => levelNets[netLevel];
    public float CurrentDeterioration => levelIce[iceLevel];
    public int CurrentTank => defaultCapacity + levelTank[tankLevel];
    public float CurrentSpeed => defaultSpeed + levelPalms[palmLevel];

    [HorizontalLine(10, EColor.Red)]
    public int netLevel = 0;
    public Netdata[] levelNets;
    public int[] netLevelPrice;
    [HorizontalLine(2, EColor.Green)]    
    public int palmLevel = 0;
    public float[] levelPalms;
    public int[] palmLevelPrice;
    [HorizontalLine(2, EColor.Green)]    
    public int iceLevel = 0;
    public float[] levelIce;
    public int[] iceLevelPrice;
    [HorizontalLine(2, EColor.Green)]    
    public int tankLevel = 0;
    public int[] levelTank;
    public int[] tankLevelPrice;

    protected override void Awake()
    {
        //remove all parent hierarchy
        transform.SetParent(null);
        IsDontDestroyOnLoad = true;
        base.Awake();
    }
    private void OnValidate()
    {
        IsDontDestroyOnLoad = true;
    }
}
