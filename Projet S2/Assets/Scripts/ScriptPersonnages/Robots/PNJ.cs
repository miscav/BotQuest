using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : Personnages
{
    private Vector3 PNJVelocity;
    [SerializeField]
    private Vector3 Destination;

    // Start is called before the first frame update
    void Start()
    {
        Character = GetComponent<CharacterController>();
        Health = 200f;
        Destination = transform.position;
        Speed = 5f;
        Damages = 10f;
        Bag = new List<Items> { };
    }

    void Update()
    {
        Coordinates = transform.position;

        if(Coordinates.x - 1 < Destination.x && Coordinates.x + 1 > Destination.x && Coordinates.z - 1 < Destination.z && Coordinates.z + 1 > Destination.z)
        {
            Destination = new Vector3(Coordinates.x + Random.Range(-10, 10), Coordinates.y, Coordinates.z + Random.Range(-10, 10));
        }

        if (Character.isGrounded && Character.velocity.y < 0)
        {
            PNJVelocity.y = 0f;
        }

        Vector3 move = new Vector3(((Destination.x - Coordinates.x) % 5) / 10, 0, ((Destination.z - Coordinates.z) % 5) / 10);

        Character.Move(transform.right * move.x);
        Character.Move(transform.forward * move.z);

        PNJVelocity.y += Gravity * Time.deltaTime;

        Character.Move(PNJVelocity * Time.deltaTime);
    }
}

