using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class son : MonoBehaviour
{
    public AudioClip sonvilage;
    float longueur;
    float prochaine;
    // Start is called before the first frame update
    void Start()
    {
        longueur = sonvilage.length;
        prochaine = Time.time + longueur;
        GetComponent<AudioSource>().PlayOneShot(sonvilage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > prochaine)
        {
            prochaine = Time.time + longueur;
            GetComponent<AudioSource>().PlayOneShot(sonvilage);
        }
    }

}
