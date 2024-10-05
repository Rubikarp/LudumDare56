using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // Référence à l'InventoryManager

    private bool shopIsOpen = false;

    void Update()
    {
        // Ouvrir ou fermer la boutique avec la touche "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }

        // Ajouter un poisson à l'inventaire avec la touche "M"
        if (Input.GetKeyDown(KeyCode.M))
        {
            InventoryManager.Fish newFish = new InventoryManager.Fish("Saumon", 50);
            inventoryManager.AddFish(newFish);
        }

        // Ajouter 100€ avec la touche "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventoryManager.AddMoney(100);
        }

        // Vendre le premier objet vendable dans l'inventaire avec la touche "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            inventoryManager.SellFirstItem();
        }
    }

    // Méthode pour ouvrir/fermer la boutique
    public void ToggleShop()
    {
        shopIsOpen = !shopIsOpen;

        if (shopIsOpen)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    // Ouvrir la boutique (affichage de message dans la console)
    void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None; // Libère le curseur
        Cursor.visible = true; // Affiche le curseur
        Debug.Log("Boutique ouverte. (Utilisez les touches pour interagir)");
    }

    // Fermer la boutique (affichage de message dans la console)
    void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur
        Debug.Log("Boutique fermée.");
    }
}
