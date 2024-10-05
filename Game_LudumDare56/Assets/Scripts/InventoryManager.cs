using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int playerMoney = 0; // Argent du joueur
    public int inventoryCapacity = 12; // Capacité maximale de l'inventaire, par défaut 12
    public int currentCapacity = 0;    // Capacité utilisée actuelle

    // Type d'objet à vendre
    [System.Serializable]
    public enum ItemType
    {
        Fish,
        Equipment
    }

    //Objet vu par le shop comme "possible à vendre"
    public interface ISellable
    {
        string Name { get; }
        int SellPrice { get; }
        ItemType ObjectType { get; }  // Type d'objet
    }

    //Objet pouvant être dans l'inventaire
    public interface IGameItem
    {
        string Name { get; }
        public void MainInteraction();
        public void SecondaryInteraction();
    }

    public List<IGameItem> Inventory = new List<IGameItem>(); // Liste des objets dans l'inventaire
}