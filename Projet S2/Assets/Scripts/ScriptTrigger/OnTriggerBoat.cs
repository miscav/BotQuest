using UnityEngine;

public class OnTriggerBoat : MonoBehaviour
{

    [SerializeField] public GameObject RepairButton;
    [SerializeField] public GameObject WinScreen;
    private bool IsIn;
    [SerializeField] public AudioClip reparation;
    private float wintime;
    private bool Repaired;

    // Start is called before the first frame update
    void Start()
    {
        IsIn= false;
        RepairButton.SetActive(false);
        WinScreen.SetActive(false);
        wintime = 0;
        Repaired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wintime != 0 && Time.time - wintime > 10)
        {
            Debug.Log("vous avez gagné");
            WinScreen.SetActive(true);
        }

        if (IsIn && Input.GetKeyDown(KeyCode.E))
        {
            Repair();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Repaired)
        {
            RepairButton.SetActive(true);
            IsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RepairButton.SetActive(false);
            IsIn = false;
        }
    }

    public void Repair()
    {
        RepairButton.SetActive(false);
        var time = Time.time;
        GetComponent<AudioSource>().PlayOneShot(reparation);
        wintime = Time.time;
        Repaired = true;
    }
}
