using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int playerMoney = 0; // Argent du joueur
    public int inventoryCapacity = 12; // Capacit� maximale de l'inventaire, par d�faut 12
    public int currentCapacity = 0;    // Capacit� utilis�e actuelle

    // Type d'objet � vendre
    [System.Serializable]
    public enum ItemType
    {
        Fish,
        Equipment
    }

    // Objet vendable
    public interface ISellable
    {
        string Name { get; }
        int SellPrice { get; }
        ItemType ObjectType { get; }  // Type d'objet
    }

    // Objet pouvant �tre dans l'inventaire
    public interface IGameItem
    {
        string Name { get; }
        void MainInteraction();
        void SecondaryInteraction();
    }

    // Classe repr�sentant un poisson
    public class Fish : IGameItem, ISellable
    {
        public string Name { get; private set; }
        public int SellPrice { get; private set; }
        public ItemType ObjectType { get; private set; }

        public Fish(string name, int sellPrice)
        {
            Name = name;
            SellPrice = sellPrice;
            ObjectType = ItemType.Fish;
        }

        public void MainInteraction()
        {
            Debug.Log("Mange le poisson " + Name);
        }

        public void SecondaryInteraction()
        {
            Debug.Log("Regarde le poisson " + Name);
        }
    }

    // Liste des objets dans l'inventaire
    public List<IGameItem> Inventory = new List<IGameItem>();

    // Ajoute un poisson � l'inventaire
    public void AddFish(Fish newFish)
    {
        if (currentCapacity < inventoryCapacity)
        {
            Inventory.Add(newFish);
            currentCapacity++;
            Debug.Log(newFish.Name + " ajout� � l'inventaire.");
        }
        else
        {
            Debug.Log("Inventaire plein !");
        }
    }

    // Ajoute de l'argent au joueur
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log(amount + "� ajout�s. Total: " + playerMoney + "�");
    }

    // Vend le premier objet vendable dans l'inventaire
    public void SellFirstItem()
    {
        foreach (var item in Inventory)
        {
            if (item is ISellable sellableItem)
            {
                Inventory.Remove(item); // Supprime l'objet de l'inventaire
                currentCapacity--; // Met � jour la capacit� utilis�e
                playerMoney += sellableItem.SellPrice; // Ajoute le prix � l'argent du joueur
                Debug.Log(sellableItem.Name + " vendu pour " + sellableItem.SellPrice + "�. Argent total : " + playerMoney + "�");
                return;
            }
        }
        Debug.Log("Aucun objet vendable trouv� dans l'inventaire.");
    }
}
