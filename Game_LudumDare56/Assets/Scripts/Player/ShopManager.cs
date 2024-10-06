using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // Référence à InventoryManager
    public GameObject shopUI; // Référence à l'UI de la boutique
    public GameObject itemLinePrefab; // Préfab pour une ligne d'item (à acheter/vendre)
    public Transform contentPanel; // Référence au panel où les items sont affichés
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur
    public Toggle buyButton; // Bouton pour "Buy"
    public Toggle sellButton; // Bouton pour "Sell"

    private bool shopIsOpen = false;
    private bool isBuyMode = false; // Faux par défaut, pour commencer en mode "Sell"

    void Start()
    {
        UpdateMoneyDisplay(); // Affiche l'argent dès le départ
        PopulateContent();
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
            Sprite fishImage = Resources.Load<Sprite>("Images/PacFish");
            InventoryManager.Fish newFish = new InventoryManager.Fish("PacFish", 50, fishImage);
            inventoryManager.AddFish(newFish);
            PopulateContent();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventoryManager.AddMoney(100);
            UpdateMoneyDisplay();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            inventoryManager.SellFirstItem();
            PopulateContent();
        }

    }

    // Méthode pour ouvrir/fermer la boutique
    public void ToggleShop()
    {
        shopIsOpen = !shopIsOpen;
        shopUI.SetActive(shopIsOpen);

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
        Cursor.lockState = CursorLockMode.None; // Libère le curseur
        Cursor.visible = true; // Affiche le curseur
        PopulateContent(); // Remplir le contenu selon l'état (Buy/Sell)
    }

    void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur
    }

    // Changer l'affichage du magasin selon l'onglet sélectionné
    public void ShowBuyItems()
    {
        isBuyMode = true; // Passer en mode "Buy"
        buyButton.interactable = false; // Désactive le bouton "Buy" (déjà sélectionné)
        sellButton.interactable = true; // Active le bouton "Sell"
        PopulateContent(); // Remplit la liste avec les items à acheter
    }

    public void ShowSellItems()
    {
        isBuyMode = false; // Passer en mode "Sell"
        buyButton.interactable = true; // Active le bouton "Buy"
        sellButton.interactable = false; // Désactive le bouton "Sell" (déjà sélectionné)
        PopulateContent(); // Remplit la liste avec les items à vendre
    }

    // Méthode pour remplir le contenu du magasin selon l'état (Buy ou Sell)
    void PopulateContent()
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
            List<InventoryManager.ISellable> sellableItems = inventoryManager.GetSellableItems();
            foreach (var item in sellableItems)
            {
                GameObject newItemLine = Instantiate(itemLinePrefab, contentPanel);
                TextMeshProUGUI itemName = newItemLine.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI itemPrice = newItemLine.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
                Image itemImage = newItemLine.transform.Find("Image").GetComponent<Image>();  // Référence à l'image dans le prefab
                Button sellButton = newItemLine.transform.Find("Button").GetComponent<Button>();

                // Mise à jour du texte et de l'image de l'item
                itemName.text = item.Name;
                itemPrice.text = item.SellPrice + "€";
                itemImage.sprite = item is InventoryManager.Fish ? (item as InventoryManager.Fish).Image : null; // Assigner l'image

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
        if (inventoryManager.playerMoney >= price)
        {
            inventoryManager.AddMoney(-price); // Retirer l'argent
            UpdateMoneyDisplay(); // Mettre à jour l'affichage de l'argent
            Debug.Log("Acheté : " + itemName);
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter " + itemName);
        }
    }

    // Vente d'un objet
    public void SellItem(InventoryManager.ISellable item)
    {
        inventoryManager.SellItem(item as InventoryManager.IGameItem); // Vente de l'objet
        PopulateContent(); // Mise à jour de la liste
        UpdateMoneyDisplay(); // Mise à jour de l'affichage de l'argent
    }

    // Mise à jour de l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        moneyText.text = inventoryManager.playerMoney + "€";
    }
}
