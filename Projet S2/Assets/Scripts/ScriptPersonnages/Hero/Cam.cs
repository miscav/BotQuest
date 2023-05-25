using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Ray ray;
    [SerializeField] GameObject Interaction;
    [SerializeField] GameObject Text;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private static int QuetesAcheve;
    [SerializeField] private QueteManagement QueteVise;
    [SerializeField] private GameObject Accepter;
    [SerializeField] private GameObject Refuser;
    [SerializeField] private GameObject Terminer;
    [SerializeField] private GameObject Abandonner;
    public AudioClip sonClic;

    void Start()
    {
        ray = new Ray(transform.position, transform.forward * 10);
        QuetesAcheve = 0;
        Text.SetActive(false);
        Terminer.SetActive(false);
        Abandonner.SetActive(false);
        Text.SetActive(false);
        Interaction.SetActive(false);
        ShopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            QuetesAcheve+= 1;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3))
        {
            if (hit.collider.gameObject.CompareTag("Quete"))
            {
                QueteVise = hit.collider.gameObject.GetComponent<QueteManagement>();
                QueteVise.init();

                if(QueteVise.quete == null) 
                {
                    Debug.Log("soucis");
                }

                Interaction.GetComponentInChildren<Text>().text = "Talk";
                Interaction.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<AudioSource>().PlayOneShot(sonClic);
                    if (QueteVise.quete.Count > 0)
                    {
                        if (QueteManagement.QuetesActuelle != null)
                        {
                            if (QueteManagement.QuetesActuelle == QueteVise.quete.Peek())
                            {
                                Text.GetComponentInChildren<Text>().text = "As tu fini la quete ?";
                                Accepter.SetActive(false);
                                Refuser.SetActive(false);
                                Terminer.SetActive(true);
                                Abandonner.SetActive(true);
                                Text.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Quete déjà en cours !");
                            }
                        }
                        else
                        {
                            Text.GetComponentInChildren<Text>().text = QueteVise.quete.Peek().text;
                            Accepter.SetActive(true);
                            Refuser.SetActive(true);
                            Terminer.SetActive(false);
                            Abandonner.SetActive(false);
                            Text.SetActive(true);
                        }
                    }
                    else
                    {
                        Debug.Log("Plus de quetes disponibles");
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("Shop"))
            {
                Interaction.GetComponentInChildren<Text>().text = "Shop";
                Interaction.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShopPanel.SetActive(true);
                }
            }
            else
            {
                Interaction.SetActive(false);
            }
        }
        else
        {
            Interaction.SetActive(false);
        }
    }

    public static int GetQueteAcheve()
    {
        return QuetesAcheve;
    }

    public void Accept()
    {
        if (QueteManagement.QuetesActuelle == null)
        {
            if (QueteVise.quete.Peek() is Principale)
            {
                if (((Principale)QueteVise.quete.Peek()).Requis == 0)
                {
                    QuetesAcheve++;
                    QueteVise.Reussi(null);
                }
                else if (((Principale)QueteVise.quete.Peek()).Requis == QuetesAcheve)
                {
                    QueteManagement.QuetesActuelle = QueteVise.quete.Peek();
                }
                else
                {
                    Debug.Log("Vous devez effectuez les quetes principales précédantes avant celle ci !");
                }
            }

            Close();
        }
        else
        {
            Debug.Log("Une quete est deja en cours !");

            Close();
        }
    }

    public void Close()
    {
        Text.SetActive(false);
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    public void Kill()
    {
        Text.SetActive(false);
        player.ReceiveDamages(player.GetHealth());
        Close();
    }

    public void Abandon()
    {
        QueteManagement.QuetesActuelle = null;
        Close();
    }

    public void Termine()
    {
        if(Inventory.instance.Search(QueteManagement.QuetesActuelle.ItemToBring) && QueteManagement.QuetesActuelle.ActionRequise)
        {
            QuetesAcheve++;
            QueteVise.Reussi(QueteManagement.QuetesActuelle.ItemToBring);
        }
        else
        {
            Debug.Log("Vous n'avez pas terminé la quete !");
        }
        Close();
    }

    public void BruitDuBouton()
    {
        GetComponent<AudioSource>().PlayOneShot(sonClic);
    }
}
