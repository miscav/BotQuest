using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : MonoBehaviour
{
    [SerializeField] private GameObject NamePanel;
    [SerializeField] private GameObject PricePanel;
    public ItemsData Item;
    // private static ItemsData ItemSelected;
    public static Inventory inventory;
    public static List<ItemsData> AllItems = new List<ItemsData>();
    public static SlotShop Instance;

    private void Start()
    {
        if (!AllItems.Contains(Item))
        {
            AllItems.Add(Item);
        }
        inventory = Inventory.instance;
        Instance = this;
    }

    public void OnClickSlotShop()
    {
        Shop.ItemSelected = Item;
        Refresh();
    }

    public void Refresh()
    {
        NamePanel.GetComponentInChildren<Text>().text = Shop.ItemSelected.Name;
        PricePanel.GetComponentInChildren<Text>().text = $"Quantité: {inventory.Qte(Shop.ItemSelected)} | {Shop.ItemSelected.Price} $";
    }
}
