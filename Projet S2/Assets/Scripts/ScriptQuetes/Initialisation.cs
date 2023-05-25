using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour 
{
    [SerializeField] public static List<Queue<Quetes>> AllQuetes = new List<Queue<Quetes>>();
    [SerializeField] public QuetePNJ Pnj1;
    [SerializeField] public QuetePNJ Pnj2;
    [SerializeField] public QuetePNJ Pnj3;
    [SerializeField] public QuetePNJ Pnj4;
    [SerializeField] public QuetePNJ Pnj5;

    public void Start()
    {
        AllQuetes.Add(Pnj1.Initialize());
        AllQuetes.Add(Pnj2.Initialize());
        AllQuetes.Add(Pnj3.Initialize());
        AllQuetes.Add(Pnj4.Initialize());
        AllQuetes.Add(Pnj5.Initialize());
    }
}
