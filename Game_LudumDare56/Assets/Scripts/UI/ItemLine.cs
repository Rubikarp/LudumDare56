using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemLine : MonoBehaviour
{
    public Image objectSprite;
    public Text objectName;
    public Text objectPrice;
    public Button actionButton;

    public void SetItem(FishItem fishItem, UnityAction action)
    {
        objectName.text = fishItem.data.specieName;
        objectSprite.sprite = fishItem.data.sprite;
        objectPrice.text = fishItem.Price.ToString() + " €";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(action);
    }
    public void SetItem(Sprite icon, string description, string price, UnityAction action)
    {
        objectName.text = description;
        objectSprite.sprite = icon;
        objectPrice.text = price + " €";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(action);
    }
}
