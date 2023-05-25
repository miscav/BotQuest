using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Article
{ 
    public int Price { get; private set; }
    public Items items { get; private set; }

    public Article(int price, Items _item)
    {
        Price = price;
        items = _item;
    }
}