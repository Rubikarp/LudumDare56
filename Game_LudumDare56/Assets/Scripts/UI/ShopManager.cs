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
    public GameObject shopScreen; // R�f�rence � l'UI de la boutique

    [Header("Content")]
    public ItemLine itemLinePrefab; // Pr�fab pour une ligne d'item (� acheter/vendre)
    public Transform contentPanel; // R�f�rence au panel o� les items sont affich�s

    [Header("Money Display")]
    public Image moneyIcon; // Ic�ne pour l'argent
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur

    [Header("Buy/Sell Buttons")]
    public Toggle buyButton; // Bouton pour "Buy"
    public Toggle sellButton; // Bouton pour "Sell"

    private bool shopIsOpen = false;
    private bool isBuyMode = false; // Faux par d�faut, pour commencer en mode "Sell"

    private void Start()
    {
        playerData = PlayerData.Instance;
        inventoryManager = PlayerInventory.Instance;

        UpdateMoneyDisplay(); // Affiche l'argent d�s le d�part
        RedrawItemLines();
        ShowBuyItems(); // Par d�faut, afficher les items � vendre
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Ouvrir/fermer la boutique
        {
            ToggleShop();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Touche M appuy�e");
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

    // M�thode pour ouvrir/fermer la boutique
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
        RedrawItemLines(); // Remplir le contenu selon l'�tat (Buy/Sell)
    }
    void CloseShop()
    {

    }

    // Changer l'affichage du magasin selon l'onglet s�lectionn�
    public void ShowBuyItems()
    {
        isBuyMode = true; // Passer en mode "Buy"
        buyButton.interactable = false; // D�sactive le bouton "Buy" (d�j� s�lectionn�)
        sellButton.interactable = true; // Active le bouton "Sell"
        RedrawItemLines(); // Remplit la liste avec les items � acheter
    }
    public void ShowSellItems()
    {
        isBuyMode = false; // Passer en mode "Sell"
        buyButton.interactable = true; // Active le bouton "Buy"
        sellButton.interactable = false; // D�sactive le bouton "Sell" (d�j� s�lectionn�)
        RedrawItemLines(); // Remplit la liste avec les items � vendre
    }

    // M�thode pour remplir le contenu du magasin selon l'�tat (Buy ou Sell)
    void RedrawItemLines()
    {
        // Vider les �l�ments actuels
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

    // Achat d'un item (cette m�thode devra �tre modifi�e selon la logique d'achat)
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

        RedrawItemLines(); // Mise � jour de la liste
        UpdateMoneyDisplay(); // Mise � jour de l'affichage de l'argent
    }

    // Mise � jour de l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        moneyText.text = playerData.money + " �";
    }
}

public enum UpgradeType
{
    Net,
    Tank,
    Icebox,
    Palmes,
}