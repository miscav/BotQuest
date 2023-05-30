using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    public static Player player;
    public static PlayerStats playerStats;
    private Ray ray;
    [SerializeField] GameObject Interaction;
    [SerializeField] GameObject Text;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] public GameObject PickUpPanel;
    [SerializeField] private QueteManagement QueteVise;
    [SerializeField] private GameObject Accepter;
    [SerializeField] private GameObject Refuser;
    [SerializeField] private GameObject Terminer;
    [SerializeField] private GameObject Abandonner;
    public AudioClip sonClic;
    [SerializeField] public GameObject ChatPanel;
    private float chattime;
    [SerializeField] public GameObject MyCreditsscenepanel;
    public bool haswin;


    [SerializeField] private GameObject WinScreen;
    [SerializeField] private AudioClip reparation;
    private float wintime;
    private bool Repaired;
    private RaycastHit hit;

    private Inventory inventory;

    void Start()
    {
        ray = new Ray(transform.position, transform.forward * 10);
        Terminer.SetActive(false);
        Abandonner.SetActive(false);
        Interaction.SetActive(false);
        Repaired = false;
        WinScreen.SetActive(false);
        wintime = 0;
        // player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        // playerStats = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerStats>();
        // player = gameObject.GetComponentInParent<Player>();
        // playerStats = gameObject.GetComponentInParent<PlayerStats>();
        ChatPanel.SetActive(false);
        chattime= 0;
        inventory = Inventory.instance;
        haswin= false;
        MyCreditsscenepanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (chattime!=0 && Time.time - chattime > 3) 
        {
            ChatPanel.SetActive(false);
            chattime = 0;
        }

        if (wintime != 0 && Time.time - wintime > 10)
        {
            player.Reparation.SetActive(false);
            WinScreen.SetActive(true);
            haswin = true;
            player.ischeated = true;
        }

        if(haswin)
        {
            MyCreditsscenepanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("CreditsScene");
            }
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3) && !Repaired)
        {
            if (hit.collider.gameObject.CompareTag("Quete"))
            {
                QueteVise = hit.collider.gameObject.GetComponent<QueteManagement>();
                QueteVise.init();


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
                                ChatPanel.GetComponent<Text>().text = "Une quête est déjà en cours !";
                                ChatPanel.SetActive(true);
                                chattime = Time.time;
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
                        ChatPanel.GetComponent<Text>().text = "Vous avez déjà fait cette quête !";
                        ChatPanel.SetActive(true);
                        chattime = Time.time;
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

                if(inventory.EnoughPieces())
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Repair();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ChatPanel.GetComponent<Text>().text = "Vous n'avez pas toutes les pièces nécéssaires";
                        ChatPanel.SetActive(true);
                        chattime = Time.time;
                    }
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

    public void Accept()
    {
        if (QueteManagement.QuetesActuelle == null)
        {
            if (QueteVise.quete.Peek() is Principale)
            {
                QueteManagement.QuetesActuelle = QueteVise.quete.Peek();
            }

            Close();
        }
        else
        {
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
        Close();
    }

    public void Abandon()
    {
        ChatPanel.GetComponent<Text>().text = "Vous avez bien abandonné la quête en cours !";
        ChatPanel.SetActive(true);
        chattime = Time.time;
        QueteManagement.QuetesActuelle = null;
        Close();
    }

    public void Termine()
    {
        if(QueteManagement.QuetesActuelle.ItemToBring == null || (Inventory.instance.Search(QueteManagement.QuetesActuelle.ItemToBring) && QueteManagement.QuetesActuelle.ActionRequise))
        {
            Initialisation.nb_quete += 1;
            ChatPanel.GetComponent<Text>().text = "Bien joué, voilà votre récompense !";
            ChatPanel.SetActive(true);
            chattime = Time.time;
            QueteVise.Reussi(QueteManagement.QuetesActuelle.ItemToBring);
            inventory.AddPiece();
        }
        else
        {
            ChatPanel.GetComponent<Text>().text = "Vous n'avez pas terminé la quete !";
            ChatPanel.SetActive(true);
            chattime = Time.time;
        }
        Close();
    }

    public void BruitDuBouton()
    {
        GetComponent<AudioSource>().PlayOneShot(sonClic);
    }

    public void Repair()
    {
        player.Reparation.SetActive(true);
        var time = Time.time;
        GetComponent<AudioSource>().PlayOneShot(reparation);
        wintime = Time.time;
        Repaired = true;
    }
}
