using Photon.Chat;
using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemsData> Content;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject SlotContent;
    [SerializeField] private GameObject ActionItemPanel;
    [SerializeField] private GameObject UseButton;
    [SerializeField] private GameObject DropButton;
    [SerializeField] private GameObject EquipButton;
    [SerializeField] private GameObject RepairButton;
    [SerializeField] private GameObject DestroyButton;
    [SerializeField] private GameObject TextPieces;
    [SerializeField] private GameObject ChatPanel;
    [SerializeField] private Sprite Transparent;
    private int Pieces;
    private int MaxPieces;
    private float time1;
    private float time2;
    private float time3;
    private float cheatedtime;

    public static Inventory instance;

    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Content = new List<ItemsData>();
        CloseActionItemPanel();
        QueteManagement.inventory = this;
        Pieces = 0;
        MaxPieces = 4;
        time1 = 0; time2 = 0; time3 = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Refresh();
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //test
            Debug.Log("premier chiffre");
            time1 = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (time1 != 0 && Time.time - time1 < 2)
                time2 = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (time2 != 0 && Time.time - time2 < 2 && time2 > time1)
                time3 = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (time3 != 0 && Time.time - time3 < 2 && time3 > time2)
            {
                Debug.Log("vous vous ête Give toute les pièces");
                ChatPanel.GetComponent<Text>().text = "vous vous ête Give toute les pièces";
                cheatedtime = Time.time;
                ChatPanel.SetActive(true);
                Pieces = MaxPieces;
            }
        }

        if (cheatedtime != 0 && Time.time - cheatedtime > 3)
        {
            ChatPanel.SetActive(false);
            cheatedtime = 0;
        }

    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        CloseActionItemPanel();
    }

    public int length()
    {
        return Content.Count;
    }

    public void Add(ItemsData item)
    {
        if(item != null && length() < 16)
        {
            Content.Add(item);
            Refresh();
        }
    }

    public bool Search(ItemsData item)
    {
        return item == null || Content.Contains(item);
    }

    public int Qte(ItemsData item)
    {
        var list = Content.FindAll(i => i.Name == item.Name);

        return list.Count;
    }

    public void Remove(ItemsData item) 
    {
        if(Search(item) && item != null)
        {
            Content.RemoveAt(Content.FindIndex(i => i.Name == item.Name));
            Refresh();
        }
        else
        {
            Debug.Log("objet non trouvé");
        }
    }

    public void Drop(ItemsData item)
    {
        Remove(item);
        GameObject drop = Instantiate(item.Prefab);
        drop.tag = "Item";
        drop.AddComponent<Items>();
        drop.GetComponent<Items>().Name = item.Name;
        drop.GetComponent<Items>().dataItem = item;
        drop.AddComponent<BoxCollider>();
        drop.transform.position = Player.player.transform.position;
    }

    public void Refresh()
    {
        TextPieces.GetComponent<Text>().text = Pieces.ToString() + " / 10 Pieces";

        for (int i = 0; i < Content.Count; i++)
        {
            Slot slot = SlotContent.transform.GetChild(i).GetComponent<Slot>();
            slot.Item = Content[i];
            slot.Visual = Content[i].Visual;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = slot.Visual;
        }
        for (int i = Content.Count; i < 16; i++)
        {
            Slot slot = SlotContent.transform.GetChild(i).GetComponent<Slot>();
            slot.Item = null;
            slot.Visual = Transparent;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = slot.Visual;
        }
    }

    public void OpenActionItemPanel(ItemsData item)
    {
        Refresh();

        switch (item.itemType)
        {
            case ItemsData.ItemType.Weapon:
                UseButton.SetActive(false);
                EquipButton.SetActive(true);
                DropButton.SetActive(true);
                RepairButton.SetActive(true);
                DestroyButton.SetActive(true);
                break;

            case ItemsData.ItemType.Tool:
                UseButton.SetActive(false);
                EquipButton.SetActive(true);
                DropButton.SetActive(true);
                RepairButton.SetActive(true);
                DestroyButton.SetActive(true);
                break;

            case ItemsData.ItemType.Food:
                UseButton.SetActive(true);
                EquipButton.SetActive(false);
                DropButton.SetActive(true);
                RepairButton.SetActive(false);
                DestroyButton.SetActive(false);
                break;

            case ItemsData.ItemType.Piece:
                UseButton.SetActive(false);
                EquipButton.SetActive(false);
                DropButton.SetActive(true);
                RepairButton.SetActive(false);
                DestroyButton.SetActive(false);
                break;
        }

        ActionItemPanel.SetActive(true);
    }

    public void CloseActionItemPanel()
    {
        ActionItemPanel.SetActive(false);
    }

    public void AddPiece()
    {
        Pieces += 1;
        Debug.Log("1");
    }

    public bool EnoughPieces()
    {
        return Pieces == MaxPieces;
    }
}
