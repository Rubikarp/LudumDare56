using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    private PlayerData playerData;
    private PlayerInventory inventoryManager;

    [SerializeField]
    private FishData debugFish;

    [HorizontalLine]
    public GameObject shopScreen; // Référence à l'UI de la boutique

    [Header("Content")]
    public ItemLine itemLinePrefab; // Préfab pour une ligne d'item (à acheter/vendre)
    public Transform contentPanel; // Référence au panel où les items sont affichés

    [Header("Money Display")]
    public Image moneyIcon; // Icône pour l'argent
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur

    [Header("Buy/Sell Buttons")]
    public Toggle buyButton; // Bouton pour "Buy"
    public Toggle sellButton; // Bouton pour "Sell"

    private bool shopIsOpen = false;
    private bool isBuyMode = false; // Faux par défaut, pour commencer en mode "Sell"

    private void Start()
    {
        playerData = PlayerData.Instance;
        inventoryManager = PlayerInventory.Instance;

        UpdateMoneyDisplay(); // Affiche l'argent dès le départ
        RedrawItemLines();
        ShowBuyItems(); // Par défaut, afficher les items à vendre
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Ouvrir/fermer la boutique
        {
            ToggleShop();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Touche M appuyée");
            var newFish = new FishItem(debugFish);
            inventoryManager.AddFish(newFish);
            RedrawItemLines();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerData.money += 100;
            UpdateMoneyDisplay();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SellItem(inventoryManager.fishCaught[0]);
            RedrawItemLines();
        }
    }

    // Méthode pour ouvrir/fermer la boutique
    public void ToggleShop()
    {
        shopIsOpen = !shopIsOpen;
        shopScreen.SetActive(shopIsOpen);

        if (shopIsOpen)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }
    void OpenShop()
    {
        RedrawItemLines(); // Remplir le contenu selon l'état (Buy/Sell)
    }
    void CloseShop()
    {

    }

    // Changer l'affichage du magasin selon l'onglet sélectionné
    public void ShowBuyItems()
    {
        isBuyMode = true; // Passer en mode "Buy"
        buyButton.interactable = false; // Désactive le bouton "Buy" (déjà sélectionné)
        sellButton.interactable = true; // Active le bouton "Sell"
        RedrawItemLines(); // Remplit la liste avec les items à acheter
    }
    public void ShowSellItems()
    {
        isBuyMode = false; // Passer en mode "Sell"
        buyButton.interactable = true; // Active le bouton "Buy"
        sellButton.interactable = false; // Désactive le bouton "Sell" (déjà sélectionné)
        RedrawItemLines(); // Remplit la liste avec les items à vendre
    }

    // Méthode pour remplir le contenu du magasin selon l'état (Buy ou Sell)
    void RedrawItemLines()
    {
        // Vider les éléments actuels
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        if (isBuyMode)
        {
            DrawUpgradeLines();
        }
        else
        {
            DrawInventoryLines();
        }
    }

    private void DrawUpgradeLines()
    {
        //For each upgrade type, draw a line
        foreach (UpgradeType upgradeType in System.Enum.GetValues(typeof(UpgradeType)))
        {
            ItemLine newItemLine = Instantiate(itemLinePrefab, contentPanel);

            var price = 0;
            switch (upgradeType)
            {
                case UpgradeType.Net:
                    price = playerData.netLevelPrice[playerData.netLevel];
                    newItemLine.SetItem(null, "Net upgrade", price.ToString(), () => BuyUpgrade(UpgradeType.Net));
                    break;
                case UpgradeType.Tank:
                    price = playerData.tankLevelPrice[playerData.tankLevel];
                    newItemLine.SetItem(null, "Tank upgrade", price.ToString(), () => BuyUpgrade(UpgradeType.Tank));
                    break;
                case UpgradeType.Icebox:
                    price = playerData.iceLevelPrice[playerData.iceLevel];
                    newItemLine.SetItem(null, "Icebox upgrade", price.ToString(), () => BuyUpgrade(UpgradeType.Icebox));
                    break;
                case UpgradeType.Palmes:
                    price = playerData.palmLevelPrice[playerData.palmLevel];
                    newItemLine.SetItem(null, "Palmes upgrade", price.ToString(), () => BuyUpgrade(UpgradeType.Palmes));
                    break;
            }
        }
    }
    private void DrawInventoryLines()
    {
        List<FishItem> sellableItems = inventoryManager.fishCaught;
        foreach (var item in sellableItems)
        {
            ItemLine newItemLine = Instantiate(itemLinePrefab, contentPanel);
            newItemLine.SetItem(item, () => SellItem(item));
        }
    }

    // Achat d'un item (cette méthode devra être modifiée selon la logique d'achat)
    void BuyUpgrade(UpgradeType upgradeType)
    {
        var price = 0;
        switch (upgradeType)
        {
            case UpgradeType.Net:
                price = playerData.netLevelPrice[playerData.netLevel];
                break;
            case UpgradeType.Tank:
                price = playerData.tankLevelPrice[playerData.tankLevel];
                break;
            case UpgradeType.Icebox:
                price = playerData.iceLevelPrice[playerData.iceLevel];
                break;
            case UpgradeType.Palmes:
                price = playerData.palmLevelPrice[playerData.palmLevel];
                break;
        }

        // Si le joueur n'a pas assez d'argent, on ne fait rien
        if (playerData.money < playerData.netLevelPrice[playerData.netLevel])
        {
            return;
        }
        playerData.money -= playerData.netLevelPrice[playerData.netLevel];
        switch (upgradeType)
        {
            case UpgradeType.Net:
                playerData.netLevel++;
                break;
            case UpgradeType.Tank:
                playerData.tankLevel++;
                break;
            case UpgradeType.Icebox:
                playerData.iceLevel++;
                break;
            case UpgradeType.Palmes:
                playerData.palmLevel++;
                break;
        }

        UpdateMoneyDisplay();
        RedrawItemLines();
    }

    // Vente d'un objet
    public void SellItem(FishItem fishItem)
    {
        inventoryManager.RemoveFish(fishItem); // Retirer l'item de l'inventaire
        playerData.money += fishItem.Price; // Retirer l'argent

        RedrawItemLines(); // Mise à jour de la liste
        UpdateMoneyDisplay(); // Mise à jour de l'affichage de l'argent
    }

    // Mise à jour de l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        moneyText.text = playerData.money + " €";
    }
}

public enum UpgradeType
{
    Net,
    Tank,
    Icebox,
    Palmes,
}