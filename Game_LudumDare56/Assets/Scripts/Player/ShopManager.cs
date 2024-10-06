using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField]
    private FishData debugFish;
    public InventoryManager inventoryManager; // Référence à InventoryManager

    [HorizontalLine]
    public GameObject shopScreen; // Référence à l'UI de la boutique

    [Header("Content")]
    public GameObject itemLinePrefab; // Préfab pour une ligne d'item (à acheter/vendre)
    public Transform contentPanel; // Référence au panel où les items sont affichés

    [Header("Money Display")]
    public Image moneyIcon; // Icône pour l'argent
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur

    [Header("Buy/Sell Buttons")]
    public Toggle buyButton; // Bouton pour "Buy"
    public Toggle sellButton; // Bouton pour "Sell"

    private bool shopIsOpen = false;
    private bool isBuyMode = false; // Faux par défaut, pour commencer en mode "Sell"

    void Start()
    {
        playerData = PlayerData.Instance;
        inventoryManager = InventoryManager.Instance;

        UpdateMoneyDisplay(); // Affiche l'argent dès le départ
        RedrawItemLines();
        ShowBuyItems(); // Par défaut, afficher les items à vendre
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Ouvrir/fermer la boutique
        {
            ToggleShop();
        }
        // Vérifie si la touche M est bien captée
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
            // Exemple d'ajout d'items à acheter avec des images
            Sprite harpoonImage = Resources.Load<Sprite>("Images/Harpoon");  // Assurez-vous que l'image est dans un dossier "Resources"
            Sprite bagImage = Resources.Load<Sprite>("Images/Bag");
            Sprite flippersImage = Resources.Load<Sprite>("Images/Flippers");

            AddItemToShop("Harpon", 200, harpoonImage);
            AddItemToShop("Grand Sac", 150, bagImage);
            AddItemToShop("Palmes", 50, flippersImage);
        }
        else
        {
            // Obtenir les objets à vendre (ceux qui sont dans l'inventaire du joueur)
            List<FishItem> sellableItems = inventoryManager.fishCaught;
            foreach (var item in sellableItems)
            {
                GameObject newItemLine = Instantiate(itemLinePrefab, contentPanel);
                TextMeshProUGUI itemName = newItemLine.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI itemPrice = newItemLine.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
                Image itemImage = newItemLine.transform.Find("Image").GetComponent<Image>();  // Référence à l'image dans le prefab
                Button sellButton = newItemLine.transform.Find("Button").GetComponent<Button>();

                // Mise à jour du texte et de l'image de l'item
                itemName.text = item.data.specieName;
                itemPrice.text = item.Price + "€";
                itemImage.sprite = item.data.sprite; // Assigner l'image

                // Adapter le texte du bouton et l'action au mode "Sell"
                sellButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sell";
                sellButton.onClick.AddListener(() => SellItem(item));
            }
        }
    }

    // Méthode pour ajouter un objet à acheter au magasin (par exemple des objets fixes)
    void AddItemToShop(string itemName, int price, Sprite itemImage)
    {
        GameObject newItemLine = Instantiate(itemLinePrefab, contentPanel);
        TextMeshProUGUI itemNameText = newItemLine.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemPriceText = newItemLine.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
        Image imageComponent = newItemLine.transform.Find("Image").GetComponent<Image>(); // Trouver l'élément Image dans le prefab
        Button buyButton = newItemLine.transform.Find("Button").GetComponent<Button>();

        // Mise à jour du texte de l'item
        itemNameText.text = itemName;
        itemPriceText.text = price + "€";
        imageComponent.sprite = itemImage; // Assigner l'image

        // Adapter le texte du bouton et l'action au mode "Buy"
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        buyButton.onClick.AddListener(() => BuyItem(itemName, price));
    }

    // Achat d'un item (cette méthode devra être modifiée selon la logique d'achat)
    void BuyItem(string itemName, int price)
    {
        if (playerData.money >= price)
        {
            playerData.money -= price; // Retirer l'argent
            UpdateMoneyDisplay(); // Mettre à jour l'affichage de l'argent
            Debug.Log("Acheté : " + itemName);
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter " + itemName);
        }
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
