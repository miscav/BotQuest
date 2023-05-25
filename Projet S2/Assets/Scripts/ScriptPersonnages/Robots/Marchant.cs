using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marchant : Personnages
{
    private Shop _shop;

    // Start is called before the first frame update
    void Start()
    {
        Health = 10f;
        Coordinates = new Vector3(0, 0, 0);
        Speed = 1f;
        Damages = 0f;
        Bag = new List<Items> { };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
