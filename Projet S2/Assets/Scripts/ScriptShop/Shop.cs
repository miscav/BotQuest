using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    public static ItemsData ItemSelected;

    public void Buy()
    {
        if (ItemSelected != null)
        {
            if (ItemSelected.Price <= Player.player.GetBalance())
            {
                if(Inventory.instance.length() < 16)
                {
                    Inventory.instance.Add(ItemSelected);
                    Player.player.AddBalance(-ItemSelected.Price);
                    SlotShop.Instance.Refresh();
                }
                else
                {
                    Debug.Log("Vous n'avez plus de place");
                }
            }
            else
            {
                Debug.Log("Vous n'avez pas assez d'argent !");
            }
        }
        else
        {
            Debug.Log("Veuillez séléctionner un object !");
        }
    }

    public void Sell()
    {
        if(ItemSelected != null)
        {
            if (Inventory.instance.Search(ItemSelected))
            {
                Inventory.instance.Remove(ItemSelected);
                Player.player.AddBalance(ItemSelected.Price);
                SlotShop.Instance.Refresh();
            }
            else
            {
                Debug.Log("Vous n'avez pas cette objet !");
            }
        }
        else
        {
            Debug.Log("Veuillez séléctionner un object !");
        }
    }

    public void Reload()
    {
        var list = SlotShop.AllItems;

        Debug.Log(list.Count);

        for(int i = 0; i < list.Count; i++)
        {
            SlotShop slot = Content.transform.GetChild(i).GetComponent<SlotShop>();

            slot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = list[i].Visual;
            slot.Item = list[i];
        }
    }
}
    