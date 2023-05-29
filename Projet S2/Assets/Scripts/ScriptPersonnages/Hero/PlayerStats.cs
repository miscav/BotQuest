using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    private Player player;
    public static PlayerStats instance;
    [SerializeField]  public AudioClip Boire;
    private float drinktime;
    private float drunktime;
    [SerializeField] GameObject ChatPannel;

    public AudioClip sonmort;
    public AudioClip sondegat;
    private bool IsAlive;
    float prochaine;
    [Header("HP")]
    [SerializeField]
    private float maxHealth = 100f;
    public float currentHealth;

    [SerializeField]
    private Image HealthFill;

    [SerializeField]
    private float HealthDecreaseRateForWaterAndHunger;

    [Header("Hunger")]
    [SerializeField]
    private float maxHunger = 100f;
    private float currentHunger;

    [SerializeField]
    private Image HungerFill;

    [SerializeField]
    private float hungerDecreaseRate;

    [Header("Water")]
    [SerializeField]
    private float maxWater = 100f;
    private float currentWater;

    [SerializeField]
    private Image WaterFill;

    [SerializeField]
    private float WaterDecreaseRate;
    private void Start()
    {
        ChatPannel = GameObject.Find("CameraPlayer").GetComponent<Cam>().ChatPanel;
        HealthFill=GameObject.Find("Canvas/ATH/HP/HealthFill").GetComponent<Image>();
        HungerFill = GameObject.Find("Canvas/ATH/Hunger/HungerFill").GetComponent<Image>();
        WaterFill = GameObject.Find("Canvas/ATH/Water/WaterFill").GetComponent<Image>();
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentWater = maxWater;
        prochaine = Time.time;
        
        IsAlive = true;
        hungerDecreaseRate= 0.5f;
        WaterDecreaseRate= 0.5f;
        HealthDecreaseRateForWaterAndHunger= 0.5f;

        instance = this;
        player = gameObject.GetComponent<Player>();
        drinktime = 0;
        ChatPannel.SetActive(false);

        Cam.playerStats = this;
    }

    private void Update()
    {
        UpdateHungerAndWaterBar();
        UpdateHPbar();
        var time =Time.time;

        if (drinktime != 0 && time - drinktime > 3)
        {
            drinktime = 0;
            currentWater = maxWater;
            ChatPannel.GetComponent<Text>().text = "Vous êtes maintenant désaltéré !";
            ChatPannel.SetActive(true);
            drunktime= Time.time;
        }

        if(drunktime != 0 && Time.time- drunktime > 3) 
        {
            drunktime = 0;
            ChatPannel.SetActive(false);
        }
    }

    private void TakeDamage(float damage, bool overTime = false)
    {
        if(Time.time > prochaine)
        {
            GetComponent<AudioSource>().PlayOneShot(sondegat);
            prochaine = Time.time + 3;
        }
        if (overTime)
        {
            currentHealth -= damage * Time.deltaTime;
        }
        else
        {
            currentHealth -= damage;
        }
    }

    private void UpdateHPbar()
    {
        HealthFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0 && IsAlive)
        {
            IsAlive = false;
            player.IsALIVE = false;
            GetComponent<AudioSource>().PlayOneShot(sonmort);
            currentHealth = maxHealth;
            currentHunger = maxHunger;
            currentWater = maxWater;
            GO.Dead();
        }
        else
        {
            IsAlive = true;
        }
    }

    private void UpdateHungerAndWaterBar()
    {
        // Diminue la faim au fil du temps et le visuel
        currentHunger -= hungerDecreaseRate * Time.deltaTime;
        HungerFill.fillAmount = currentHunger / maxHunger;

        // empêche de passer le négatif
        currentHunger = currentHunger < 0 ? 0 : currentHunger;
        currentWater = currentWater < 0 ? 0 : currentWater;
        // Diminue la soif au fil du temps et le visuel 
        currentWater -= WaterDecreaseRate * Time.deltaTime;
        WaterFill.fillAmount = currentWater / maxWater;

        // si barre faim / soif à zéro, retire des hp ( *2 pour les deux à 0)
        if (currentHunger <= 0 || currentWater <= 0)
        {
            TakeDamage((currentHunger <= 0 & currentWater <= 0 ? HealthDecreaseRateForWaterAndHunger * 2 : HealthDecreaseRateForWaterAndHunger),true);
        }
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Drink()
    {
        GetComponent<AudioSource>().PlayOneShot(Boire);
        drinktime = Time.time;
        currentWater = maxWater;
    }

    public void Damages(int damage)
    {
        currentHealth -= damage;
    }
}
