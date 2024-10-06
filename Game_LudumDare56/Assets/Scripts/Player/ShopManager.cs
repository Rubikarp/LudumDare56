using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // Référence à InventoryManager
    public GameObject shopUI; // Référence à l'UI de la boutique
    public GameObject itemLinePrefab; // Préfab pour une ligne d'item (à vendre)

    public Transform contentPanel; // Référence au panel où les items sont affichés
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur

    private bool shopIsOpen = false;

    void Start()
    {
        UpdateMoneyDisplay(); // Affiche l'argent dès le départ
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
            InventoryManager.Fish newFish = new InventoryManager.Fish("Saumon", 50);
            inventoryManager.AddFish(newFish);
            PopulateShop();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventoryManager.AddMoney(100);
            UpdateMoneyDisplay();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            inventoryManager.SellFirstItem();
            PopulateShop();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
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
    }

    // Ouvrir la boutique
    void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None; // Libère le curseur
        Cursor.visible = true; // Affiche le curseur
        PopulateShop(); // Remplit la liste d'items à vendre
    }

    // Remplir le magasin avec les objets vendables de l'inventaire
    void PopulateShop()
    {
        // Vider les éléments actuels
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Obtenir la liste des objets vendables
        List<InventoryManager.ISellable> sellableItems = inventoryManager.GetSellableItems();

        foreach (var item in sellableItems)
        {
            GameObject newItemLine = Instantiate(itemLinePrefab, contentPanel);
            TextMeshProUGUI itemName = newItemLine.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemPrice = newItemLine.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
            Button sellButton = newItemLine.transform.Find("Button").GetComponent<Button>();

            // Mise à jour du texte de l'item
            itemName.text = item.Name;
            itemPrice.text = item.SellPrice + "€";

            // Associer la fonction de vente au bouton
            sellButton.onClick.AddListener(() => SellItem(item));
        }
    }

    // Méthode pour vendre un objet
    public void SellItem(InventoryManager.ISellable item)
    {
        inventoryManager.SellItem(item as InventoryManager.IGameItem); // Vente de l'objet
        PopulateShop(); // Mise à jour de la liste
        UpdateMoneyDisplay(); // Mise à jour de l'affichage de l'argent
    }

    // Mise à jour de l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        moneyText.text = inventoryManager.playerMoney + "€";
    }
}
