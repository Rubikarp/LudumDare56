using TMPro;    
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class InventoryCase : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Button discardButton;
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI fishName;
    [SerializeField] private Image freshnessGauge;

    public FishItem fishItem;

    private void Awake()
    {
        discardButton.onClick.AddListener(DiscardFish);
    }
    public void SetFishItem(FishItem fishItem)
    {
        this.fishItem = fishItem;
        icon.sprite = fishItem.data.sprite;
        fishName.text = fishItem.data.specieName;

        float freshness = Mathf.Clamp01(1 - (Time.time - fishItem.caughtTime) / fishItem.data.defaultFreshness);
        freshnessGauge.fillAmount = freshness;
    }

    public void DiscardFish()
    {
        if(fishItem == null)
        {
            Debug.LogWarning("No fish to discard");
            return;
        }
        PlayerInventory.Instance.RemoveFish(fishItem);
        PlayerInventory.Instance.UpdateHUD();
    }
}