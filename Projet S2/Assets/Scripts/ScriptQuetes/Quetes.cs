using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quetes : ScriptableObject
{
    [SerializeField] public ItemsData RewardItem;
    [SerializeField] public int RewardMoney;
    [SerializeField] public string text;
    [SerializeField] public ItemsData ItemToBring;
    [SerializeField] public bool ActionRequise;
}
