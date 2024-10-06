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

    [MinMaxSlider(20, 60)]
    public Vector2Int minMaxSize;
    [MinMaxSlider(500, 3000)]
    public Vector2Int minMaxWeight;
}
