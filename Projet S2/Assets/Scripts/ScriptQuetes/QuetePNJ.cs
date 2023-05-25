using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetePNJ : MonoBehaviour
{
    [SerializeField] public Quetes Quete1;
    [SerializeField] public Quetes Quete2;
    [SerializeField] public Quetes Quete3;
    [SerializeField] public Quetes Quete4;
    [SerializeField] public Quetes Quete5;

    public Queue<Quetes> Initialize()
    {
        Queue<Quetes> queue = new Queue<Quetes>();
        if (Quete1 != null) queue.Enqueue(Quete1);
        if (Quete2 != null) queue.Enqueue(Quete2);
        if (Quete3 != null) queue.Enqueue(Quete3);
        if (Quete4 != null) queue.Enqueue(Quete4);
        if (Quete5 != null) queue.Enqueue(Quete5);

        return queue;
    }
}
