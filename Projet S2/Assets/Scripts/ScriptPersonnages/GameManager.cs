using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name,new Vector3(430,2,415),Quaternion.identity, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            OnApplicationQuit();
        }
    }

    public void OnPlayerEnterRoom(Player other)
    {
        print(other.name + " just connected !");
    }

    public void OnPlayerLeftRoom(Player other)
    {
        print(other.name + " just disconneted !");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("SceneLauncher");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }


}
