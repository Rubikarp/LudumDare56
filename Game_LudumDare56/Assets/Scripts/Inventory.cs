using System.Collections;
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

    //Objet vu par le shop comme "possible � vendre"
    public interface ISellable
    {
        string Name { get; }
        int SellPrice { get; }
        ItemType ObjectType { get; }  // Type d'objet
    }

    //Objet pouvant �tre dans l'inventaire
    public interface IGameItem
    {
        string Name { get; }
        public void MainInteraction();
        public void SecondaryInteraction();
    }

    public List<IGameItem> Inventory = new List<IGameItem>(); // Liste des objets dans l'inventaire
}