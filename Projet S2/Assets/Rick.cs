using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rick : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioClip Ricklol;
    [SerializeField] GameObject ChatPannel;
    void Start()
    {
        ChatPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(Ricklol);
            ChatPannel.GetComponent<Text>().text = "Your curiosity just got you Rick Rolled lol";
            ChatPannel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Stop();
            ChatPannel.SetActive(false);
        }
    }
}
