using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // Référence vers l'InventoryManager
    public GameObject shopUI; // Référence à l'UI du shop

    private bool shopIsOpen = false;

    void Update()
    {
        // Ouvrir ou fermer la boutique avec la touche "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
    }

    // Méthode pour ouvrir/fermer la boutique
    public void ToggleShop()
    {
        shopIsOpen = !shopIsOpen;
        shopUI.SetActive(shopIsOpen); // Active ou désactive l'UI de la boutique

        if (shopIsOpen)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    // Ouvrir la boutique
    void OpenShop()
    {
        // Empêche les déplacements du joueur
        // (ici, il faudrait une référence à un script qui contrôle le joueur)
        Cursor.lockState = CursorLockMode.None; // Libère le curseur
        Cursor.visible = true; // Affiche le curseur

        Debug.Log("Boutique ouverte.");
        DisplaySellableItems(); // Affiche les objets vendables dans la boutique
    }

    // Fermer la boutique
    void CloseShop()
    {
        // Réactive les déplacements du joueur
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur

        Debug.Log("Boutique fermée.");
    }

    // Affiche les objets vendables dans l'inventaire
    void DisplaySellableItems()
    {
        foreach (var item in inventoryManager.Inventory)
        {
            // Vérifie si l'objet implémente ISellable
            if (item is InventoryManager.ISellable sellableItem)
            {
                Debug.Log("Objet vendable: " + sellableItem.Name + " | Prix: " + sellableItem.SellPrice);
                // Ici, tu pourrais créer des boutons ou des éléments d'UI pour chaque objet vendable
                // Par exemple, ajouter à une liste d'UI dynamique pour permettre de les vendre.
            }
        }
    }

    // Méthode pour vendre un objet spécifique
    public void SellItem(InventoryManager.ISellable itemToSell)
    {
        if (inventoryManager.Inventory.Contains(itemToSell as InventoryManager.IGameItem))
        {
            // Ajoute l'argent du joueur
            inventoryManager.playerMoney += itemToSell.SellPrice;

            // Retire l'objet de l'inventaire
            inventoryManager.Inventory.Remove(itemToSell as InventoryManager.IGameItem);
            inventoryManager.currentCapacity--; // Met à jour la capacité utilisée

            Debug.Log(itemToSell.Name + " vendu pour " + itemToSell.SellPrice + " pièces.");
        }
        else
        {
            Debug.Log("Objet non trouvé dans l'inventaire.");
        }
    }
}