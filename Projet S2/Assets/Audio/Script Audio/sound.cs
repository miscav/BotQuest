using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip musique;
    float longueur;
    float prochaine;
    
    // Start is called before the first frame update
    void Start()
    {
        longueur = musique.length;
        prochaine = Time.time + longueur;
        GetComponent<AudioSource>().PlayOneShot(musique);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > prochaine)
        {
            prochaine = Time.time + longueur;
            GetComponent<AudioSource>().PlayOneShot(musique);
        }
    }
}
