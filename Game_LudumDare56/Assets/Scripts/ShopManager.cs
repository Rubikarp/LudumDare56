using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // R�f�rence vers l'InventoryManager
    public GameObject shopUI; // R�f�rence � l'UI du shop

    private bool shopIsOpen = false;

    void Update()
    {
        // Ouvrir ou fermer la boutique avec la touche "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
    }

    // M�thode pour ouvrir/fermer la boutique
    public void ToggleShop()
    {
        shopIsOpen = !shopIsOpen;
        shopUI.SetActive(shopIsOpen); // Active ou d�sactive l'UI de la boutique

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
        // Emp�che les d�placements du joueur
        // (ici, il faudrait une r�f�rence � un script qui contr�le le joueur)
        Cursor.lockState = CursorLockMode.None; // Lib�re le curseur
        Cursor.visible = true; // Affiche le curseur

        Debug.Log("Boutique ouverte.");
        DisplaySellableItems(); // Affiche les objets vendables dans la boutique
    }

    // Fermer la boutique
    void CloseShop()
    {
        // R�active les d�placements du joueur
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur

        Debug.Log("Boutique ferm�e.");
    }

    // Affiche les objets vendables dans l'inventaire
    void DisplaySellableItems()
    {
        foreach (var item in inventoryManager.Inventory)
        {
            // V�rifie si l'objet impl�mente ISellable
            if (item is InventoryManager.ISellable sellableItem)
            {
                Debug.Log("Objet vendable: " + sellableItem.Name + " | Prix: " + sellableItem.SellPrice);
                // Ici, tu pourrais cr�er des boutons ou des �l�ments d'UI pour chaque objet vendable
                // Par exemple, ajouter � une liste d'UI dynamique pour permettre de les vendre.
            }
        }
    }

    // M�thode pour vendre un objet sp�cifique
    public void SellItem(InventoryManager.ISellable itemToSell)
    {
        if (inventoryManager.Inventory.Contains(itemToSell as InventoryManager.IGameItem))
        {
            // Ajoute l'argent du joueur
            inventoryManager.playerMoney += itemToSell.SellPrice;

            // Retire l'objet de l'inventaire
            inventoryManager.Inventory.Remove(itemToSell as InventoryManager.IGameItem);
            inventoryManager.currentCapacity--; // Met � jour la capacit� utilis�e

            Debug.Log(itemToSell.Name + " vendu pour " + itemToSell.SellPrice + " pi�ces.");
        }
        else
        {
            Debug.Log("Objet non trouv� dans l'inventaire.");
        }
    }
}