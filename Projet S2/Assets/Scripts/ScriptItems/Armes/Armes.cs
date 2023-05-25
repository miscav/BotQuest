using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arme", menuName = "Item/New Arme")]

public class Armes : ItemsData
{
    [SerializeField] public int Degat;
    [SerializeField] public int Portee;
    [SerializeField] public int Duramax;

    // Axe, Bow, Sword, Torch
}