using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemsData Item;
    public static ItemsData ItemSelected;
    public Sprite Visual;
    [SerializeField] private GameObject Player;


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
        Player.GetComponent<Player>().Eat(ItemSelected); 
    }
    public void Drop()
    {
        Inventory.instance.Drop(ItemSelected);
    }
}
