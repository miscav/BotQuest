using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour 
{
    [SerializeField] public static List<Queue<Quetes>> AllQuetes;
    [SerializeField] public QuetePNJ Pnj1;
    [SerializeField] public QuetePNJ Pnj2;

    public static int nb_quete;

    public void Start()
    {
        AllQuetes = new List<Queue<Quetes>>();
        AllQuetes.Add(Pnj1.Initialize());
        AllQuetes.Add(Pnj2.Initialize());

        nb_quete = 0;
    }
}
