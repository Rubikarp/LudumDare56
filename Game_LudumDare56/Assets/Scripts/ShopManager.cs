using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField]
    private bool shopIsOpen = false;

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }
    void Update()
    {
        // Ouvrir ou fermer la boutique avec la touche "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }

    }

    public void SellFish(FishData fish)
    {
        playerData.money += fish.Price;
    }
    public void BuyUpgrade(int price)
    {
        playerData.money -= price;
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
        Debug.Log("Boutique ouverte. (Utilisez les touches pour interagir)");
    }
    // Fermer la boutique (affichage de message dans la console)
    void CloseShop()
    {
        Debug.Log("Boutique fermée.");
    }
}
