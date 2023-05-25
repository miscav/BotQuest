using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robots : Personnages
{
    public Quetes Quete { get; private set; }
    public int Reward { get; private set; }
    void Start()
    {
        Health = 10f;
        Coordinates = new Vector3(0, 0, 0); // a definir 
        Speed = 1f;
        Damages = 0f;
        Bag = new List<Items> {  };
        Reward = 2000;
    }

    public void QueteCompleted(Player player)
    {
    }
}
