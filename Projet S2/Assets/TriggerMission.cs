using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMission : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Initialisation.nb_quete == 2 && QueteManagement.QuetesActuelle != null && QueteManagement.QuetesActuelle is Principale)
            {
                QueteManagement.QuetesActuelle.ActionRequise = true;
            }
        }
    }
}
