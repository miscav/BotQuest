using UnityEngine;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void Buy(int index)
    {
        if (AllItems.All[index].Price > Player.player.GetBalance())
        {

        }
    }
}