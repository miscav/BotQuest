using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible= false;
        //Cursor.lockState= CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("SceneLauncher");
        }
    }
}
