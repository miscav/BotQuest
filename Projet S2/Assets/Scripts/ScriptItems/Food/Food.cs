using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Item/New Food")]

public class Food : ItemsData
{
    [SerializeField] public int PV;
    [SerializeField] public int Faim;

    // Apple, Banana, Fish, Meat, Vegetable
}