using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;



public class GO : MonoBehaviour
{
    private static GameObject GameOverPanel;
    private static GameObject Spawn;
    public static Player player;
    public static PlayerStats playerStats;
    public static AudioClip sonmort;



    // Start is called before the first frame update
    void Start()
    {
        Spawn = GameObject.FindWithTag("Spawn");
        GameOverPanel = gameObject;
        GameOverPanel.SetActive(false);
    }

    public static void Dead()
    {
        GameOverPanel.SetActive(true);
    }

    public static void Respawn()
    {
        GameOverPanel.SetActive(false);
        player.gameObject.transform.position = Spawn.transform.position;
        player.IsALIVE = true;
        playerStats.ResetStat();
    }
}
