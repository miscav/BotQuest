using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class QueteManagement : MonoBehaviour
{
    [SerializeField] public Queue<Quetes> quete;
    [SerializeField] public int pnj;
    [SerializeField] public static Quetes QuetesActuelle;
    [SerializeField] public static Player player;
    [SerializeField] public static Inventory inventory;
    [SerializeField] public bool Init;
    [SerializeField] private GameObject quetepanel;
        
    private void Start()
    {
        Init = false;
        QuetesActuelle = null;
        quetepanel.SetActive(false);
    }

    private void Update()
    {
        if(QuetesActuelle == null)
        {
            quetepanel.SetActive(false);
        }
        else
        {
            quetepanel.SetActive(true);
        }
    }

    public void Reussi(ItemsData itemToBring)
    {
        if(itemToBring != null)
        {
            inventory.Remove(itemToBring);
        }
        Quetes succeed = quete.Dequeue();
        QuetesActuelle = null;
        player.AddBalance(succeed.RewardMoney);
        inventory.Add(succeed.RewardItem);
    }

    public void init()
    {
        quete = Initialisation.AllQuetes[pnj];
        Init = true;
    }
}