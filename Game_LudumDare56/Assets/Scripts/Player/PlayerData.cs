using UnityEngine;
using NaughtyAttributes;

public class PlayerData : SingletonMonoBehaviour<PlayerData>
{
    public int money = 0;

    [HorizontalLine]
    public int defaultCapacity = 8;
    public float defaultSpeed = 5f;
    public float defaultDeterioration = 1f;

    [HorizontalLine]
    public int netLevel = 0;
    public int iceLevel = 0;
    public int palmLevel = 0;
    public int tankLevel = 0;

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
