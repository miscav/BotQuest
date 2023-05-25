using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatCode : MonoBehaviour
{
    public float time1;
    public float time2;
    public float time3;
    public Transform teleport;
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        time1 = 0; time2 = 0; time3 = 0;
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool cheat(Input input)
    {
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
                Debug.Log("vous vous maintenant cheaté");
                return true;
            }
        }
        return false;
    }
}
