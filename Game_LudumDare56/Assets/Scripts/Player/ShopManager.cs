using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // R�f�rence � InventoryManager
    public GameObject shopUI; // R�f�rence � l'UI de la boutique
    public GameObject itemLinePrefab; // Pr�fab pour une ligne d'item (� vendre)

    public Transform contentPanel; // R�f�rence au panel o� les items sont affich�s
    public TextMeshProUGUI moneyText; // Texte pour afficher l'argent du joueur

    private bool shopIsOpen = false;

    void Start()
    {
        UpdateMoneyDisplay(); // Affiche l'argent d�s le d�part
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Ouvrir/fermer la boutique
        {
            ToggleShop();
        }

        // V�rifie si la touche M est bien capt�e
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Touche M appuy�e");
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

    // M�thode pour ouvrir/fermer la boutique
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
        Cursor.lockState = CursorLockMode.None; // Lib�re le curseur
        Cursor.visible = true; // Affiche le curseur
        PopulateShop(); // Remplit la liste d'items � vendre
    }

    // Remplir le magasin avec les objets vendables de l'inventaire
    void PopulateShop()
    {
        // Vider les �l�ments actuels
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

            // Mise � jour du texte de l'item
            itemName.text = item.Name;
            itemPrice.text = item.SellPrice + "�";

            // Associer la fonction de vente au bouton
            sellButton.onClick.AddListener(() => SellItem(item));
        }
    }

    // M�thode pour vendre un objet
    public void SellItem(InventoryManager.ISellable item)
    {
        inventoryManager.SellItem(item as InventoryManager.IGameItem); // Vente de l'objet
        PopulateShop(); // Mise � jour de la liste
        UpdateMoneyDisplay(); // Mise � jour de l'affichage de l'argent
    }

    // Mise � jour de l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        moneyText.text = inventoryManager.playerMoney + "�";
    }
}
