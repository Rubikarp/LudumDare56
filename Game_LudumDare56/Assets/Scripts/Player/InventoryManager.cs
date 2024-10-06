using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private PlayerData playerData;
    public List<FishData> fishCaught = new List<FishData>();

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

}
