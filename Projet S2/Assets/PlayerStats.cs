
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public GameObject DrinkButton;
    [SerializeField] public GameObject DrunkScreen;
    private float ScreenTime;
    private bool IsIn;
    [SerializeField] public AudioClip Drinking;
    private float Drinktime;
    public Player player;

    public AudioClip sonmort;
     public AudioClip sondegat;
    public bool IsAlive;
     float prochaine;
    [Header("HP")]
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

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
    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentWater = maxWater;
        prochaine = Time.time;
        IsAlive = true;

        IsIn = false;
        DrinkButton.SetActive(false);
        DrunkScreen.SetActive(false);
        Drinktime = 0;
        ScreenTime = 0;
        hungerDecreaseRate= 0.5f;
        WaterDecreaseRate= 0.5f;
        HealthDecreaseRateForWaterAndHunger= 0.5f;
    }

    void Update()
    {

        if (Drinktime != 0 && Time.time - Drinktime > 3)
        {
            Debug.Log("vous avez bu");
            DrunkScreen.SetActive(true);
            currentWater = maxWater;
            ScreenTime = Time.time;
            Drinktime = 0;
        }

        if (ScreenTime != 0 && Time.time - ScreenTime > 3)
        {
            Debug.Log("panel desactivé");
            DrunkScreen.SetActive(false);
            ScreenTime = 0;
        }

        if (IsIn && Input.GetKeyDown(KeyCode.E))
        {
            Drink();
        }

        UpdateHungerAndWaterBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DrinkButton.SetActive(true);
            IsIn = true;
        }
    }

    void TakeDamage(float damage, bool overTime = false)
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

        if(currentHealth <= 0 && IsAlive)
        {
            IsAlive = false;
            GetComponent<AudioSource>().PlayOneShot(sonmort);
            Debug.Log("Player died");
        }
        UpdateHPbar();
    }

    void UpdateHPbar()
    {
        HealthFill.fillAmount = currentHealth / maxHealth;
    }

    void UpdateHungerAndWaterBar()
    {
        Debug.Log("uptade test");
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


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DrinkButton.SetActive(false);
            IsIn = false;
        }
    }

    public void Drink()
    {
        DrinkButton.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(Drinking);
        Drinktime = Time.time;
    }
}
