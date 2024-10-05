using UnityEngine;
public class Inventory : MonoBehaviour
{
    public int currentItem = 0;
    public int totalItems = 5; // Nombre d'items dans l'inventaire

    void Update()
    {
        // Scroll vers le haut ou le bas pour changer d'item
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentItem = (currentItem + 1) % totalItems;
        }
        else if (scroll < 0f)
        {
            currentItem = (currentItem - 1 + totalItems) % totalItems;
        }

        // Afficher l'item sélectionné (logique à implémenter)
        Debug.Log("Item sélectionné : " + currentItem);
    }
}
