using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Fish Data")]
public class FishData : ScriptableObject
{
    [ShowAssetPreview]
    public Sprite sprite;
    public FishSpecies specie;
    [ShowNativeProperty]
    public string specieName => specie.ToString();

    [Range(60, 240)]
    public float defaultFreshness;
    public int basePrice = 10;
}
