using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellTriger : MonoBehaviour
{
    /*[SerializeField] public GameObject DrinkButton;
    [SerializeField] public GameObject DrunkScreen;
    private float ScreenTime;
    private bool IsIn;
    [SerializeField] public AudioClip Drinking;
    private float Drinktime;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        IsIn = false;
        DrinkButton.SetActive(false);
        DrunkScreen.SetActive(false);
        Drinktime = 0;
        ScreenTime= 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drinktime != 0 && Time.time - Drinktime > 3)
        {
            Debug.Log("vous avez bu");
            DrunkScreen.SetActive(true);
            ScreenTime= Time.time;
            Drinktime = 0;
        }
        
        if(ScreenTime != 0 && Time.time - ScreenTime > 3)
        {
            Debug.Log("panel desactivé");
            DrunkScreen.SetActive(false);
            ScreenTime= 0;
        }

        if (IsIn && Input.GetKeyDown(KeyCode.E))
        {
            Drink();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DrinkButton.SetActive(true);
            IsIn = true;
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
    }*/
}
