using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ItemLine : MonoBehaviour
{
    public Image objectSprite;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;
    public Button actionButton;

    public void SetItem(FishItem fishItem, UnityAction action)
    {
        itemName.text = fishItem.data.specieName;
        objectSprite.sprite = fishItem.data.sprite;
        itemPrice.text = fishItem.Price.ToString() + " €";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(action);
    }
    public void SetItem(Sprite icon, string description, string price, UnityAction action)
    {
        if (icon != null) objectSprite.sprite = icon;

        itemName.text = description;
        itemPrice.text = price + " €";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(action);
    }
}
