using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    public ItemsData Boussole;
    public ItemsData Banana;
    public ItemsData fishing_rod;
    public static List<ItemsData> All;

    private void Start()
    {
        All = new List<ItemsData>();
        All.Add(Boussole); 
        All.Add(Banana);
        All.Add(fishing_rod);
    }
}
