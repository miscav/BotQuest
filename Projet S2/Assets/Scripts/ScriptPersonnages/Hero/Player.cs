using Photon.Chat;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Personnages
{
    private int Balance;
    public int GetBalance() { return Balance; }
    private float run = 1;
    private Vector3 playerVelocity;
    protected float Hungry;
    public AudioClip sonmarche;
    public float delaybetweenstep;
    private float nextPlay;
    [SerializeField] private Image Boussole;
    private bool IsGrounded;
    public bool ischeated;
    private float time1;
    private float time2;
    private float time3;
    private float cheatedtime;
    public float rotateSpeed = 180.0f;
    [SerializeField] private GameObject ChatPannel;

    [SerializeField] private GameObject BalancePanel;

    public static Player player;

    void Start()
    {
        BalancePanel = GameObject.Find("Canvas/BalancePanel");
        ChatPannel = GameObject.Find("CameraPlayer").GetComponent<Cam>().ChatPanel;
        Boussole = GameObject.Find("Canvas/Boussole").GetComponent<Image>();

        Debug.Log(ChatPannel.name);
        IsGrounded = false;
        Health = 200f;
        Coordinates = new Vector3(0, 0, 0);
        Speed = 5f;
        Damages = 10f;
        Bag = new List<Items> { };
        Character = GetComponent<CharacterController>();
        Hungry = 100;
        QueteManagement.player = this;
        delaybetweenstep = 0.65f;
        time1 = 0; time2 = 0; time3 = 0;
        player = this;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                AddBalance(1000);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                time1 = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (time1 != 0 && Time.time - time1 < 2)
                    time2 = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (time2 != 0 && Time.time - time2 < 2 && time2 > time1)
                    time3 = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (time3 != 0 && Time.time - time3 < 2 && time3 > time2)
                {
                    Debug.Log("vous vous désormais cheaté");
                    ChatPannel.GetComponent<Text>().text = "With great power comes great responsibility";
                    cheatedtime = Time.time;
                    ChatPannel.SetActive(true);
                    ischeated = true;
                    Speed = Speed * 4;
                }
            }

            if (cheatedtime != 0 && Time.time - cheatedtime > 3)
            {
                ChatPannel.SetActive(false);
                cheatedtime = 0;
            }

            if (Cam.GetQueteAcheve() > 1)
            {
                Boussole.gameObject.SetActive(true);
            }
            else
            {
                Boussole.gameObject.SetActive(false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                run = 1.5f;
            }

            Coordinates = transform.position;

            if (Character.velocity.y < 0 && Character.isGrounded) playerVelocity.y = 0f;

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Character.Move(transform.right * move.x * Time.deltaTime * Speed * run);
            Character.Move(transform.forward * move.z * Time.deltaTime * Speed * run);
            ReduiceHungry(Time.deltaTime * run);

            if (move != new Vector3(0, 0, 0) && Time.time > nextPlay && Character.isGrounded)
            {
                nextPlay = Time.time + delaybetweenstep;
                GetComponent<AudioSource>().PlayOneShot(sonmarche);
            }

            if (Character.isGrounded || IsGrounded)
            {
                IsGrounded = true;

            }

            // Changes the height position of the player..
            if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded || ischeated))
            {
                IsGrounded = false;
                playerVelocity.y += -0.7f * Gravity;
            }

            playerVelocity.y += Gravity * Time.deltaTime;

            Character.Move(playerVelocity * Time.deltaTime);

            run = 1f;

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * rotateSpeed);
            Boussole.transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse X")) * Time.deltaTime * rotateSpeed);
        }
        
    }

    public void ReduiceHungry(float hungry)
    {
        if (Hungry <= 0f)
        {
            Hungry = 0f;
            ReceiveDamages(5*Time.deltaTime);
        }
        else
        {
            Hungry -= hungry*Time.deltaTime * 0.1f;
        }
    }

    public void AddBalance(int balance)
    {
        Balance += balance;
        RefreshBalance();
    }

    public void Eat(ItemsData item)
    {
        if(item is Food)
        {
            if (Health == MaxHealth)
            {
                // Ne peux pas manger
            }
            else
            {
                Health += ((Food)item).PV;
            }
        }
    }

    private void RefreshBalance()
    {
        string BalanceText = "";
        string Bal = Balance.ToString();
        int l = Bal.Length;

        for (int i = l-1;i >= 0; i--)
        {
            BalanceText = Bal[i] + BalanceText;

            if ((l - i) % 3 == 0 && l - 1 != i)
            {
                BalanceText = " " + BalanceText;
            }
        }

        BalancePanel.GetComponentInChildren<Text>().text = $"{BalanceText} $";
    }
}

