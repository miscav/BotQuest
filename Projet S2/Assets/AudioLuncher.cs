using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLuncher : MonoBehaviour
{

    [SerializeField] public AudioClip musique;
    private float debut;
    private float duree;

    // Start is called before the first frame update
    void Start()
    {
        duree = musique.length;
        GetComponent<AudioSource>().PlayOneShot(musique);
        debut = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - debut > duree)
        {
            GetComponent<AudioSource>().PlayOneShot(musique);
            debut = Time.time;
        }
    }
}
