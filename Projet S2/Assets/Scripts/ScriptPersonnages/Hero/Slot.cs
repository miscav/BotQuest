using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemsData Item;
    public static ItemsData ItemSelected;
    public Sprite Visual;
    public static PlayerStats Playerstat;


    public void OnClickSlot()
    {
        ItemSelected = Item;
        Inventory.instance.OpenActionItemPanel(ItemSelected);
    }

    public void Destroy()
    {
        Inventory.instance.Remove(ItemSelected);
    }
    public void Use()
    {
        Playerstat.Eat(ItemSelected);
        Inventory.instance.Remove(ItemSelected);
    }
    public void Drop()
    {
        Inventory.instance.Drop(ItemSelected);
    }
}
