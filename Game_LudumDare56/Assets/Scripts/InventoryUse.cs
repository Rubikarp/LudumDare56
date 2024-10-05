using UnityEngine;
public class InventoryUse : MonoBehaviour
{
    public Inventory inventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic droit
        {
            UseItem(inventory.currentItem); // Utiliser l'item sélectionné
        }
    }

    void UseItem(int itemIndex)
    {
        // Implémente la logique pour utiliser l'item ici
        Debug.Log("Utilisation de l'item : " + itemIndex);
    }
}
