using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    private Player player;
    private PlayerStats playerStats;
    private Ray ray;
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

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private AudioClip reparation;
    private float wintime;
    private bool Repaired;


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
        Repaired = false;
        WinScreen.SetActive(false);
        wintime = 0;
        player = gameObject.GetComponentInParent<Player>();
        playerStats = gameObject.GetComponentInParent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            QuetesAcheve+= 1;
        }

        if (wintime != 0 && Time.time - wintime > 10)
        {
            Debug.Log("vous avez gagné");
            WinScreen.SetActive(true);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3) && !Repaired)
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
                hit.collider.gameObject.GetComponent<Shop>().Reload();
                Interaction.GetComponentInChildren<Text>().text = "Shop";
                Interaction.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShopPanel.SetActive(true);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Drink"))
            {
                Interaction.GetComponentInChildren<Text>().text = "Drink";
                Interaction.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerStats.Drink();
                }
            }
            else if (hit.collider.gameObject.CompareTag("Port"))
            {
                Interaction.GetComponentInChildren<Text>().text = "Repair";
                Interaction.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Repair();
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

    private void Accept()
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

    private void Close()
    {
        Text.SetActive(false);
    }

    private void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    private void Kill()
    {
        Text.SetActive(false);
        player.ReceiveDamages(player.GetHealth());
        Close();
    }

    private void Abandon()
    {
        QueteManagement.QuetesActuelle = null;
        Close();
    }

    private void Termine()
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

    private void BruitDuBouton()
    {
        GetComponent<AudioSource>().PlayOneShot(sonClic);
    }

    private void Repair()
    {
        var time = Time.time;
        GetComponent<AudioSource>().PlayOneShot(reparation);
        wintime = Time.time;
        Repaired = true;
    }
}
