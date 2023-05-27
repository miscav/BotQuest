using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    static public GameManager Instance;
    private GameObject instance;
    private GameObject Cam;
    public static bool isLeavingRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Instance = this;
        Cam = GameObject.Find("CameraPlayer");
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("SceneLauncher");
            return;
        }
        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
        instance=PhotonNetwork.Instantiate(this.playerPrefab.name,new Vector3(430,2,415),Quaternion.identity, 0);
        instance.tag = "Player";
        Cam.transform.SetParent(instance.transform, false);

    }

    void Update()
    {
        if (!isLeavingRoom)
        {
            // Quit the Scene if Esc
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                LeaveRoom();
            }
        }
    }

    public void OnPlayerEnterRoom(Player other)
    {
        print(other.name + " just connected !");
    }

    public void OnPlayerLeftRoom(Player other)
    {
        print(other.name + " just disconnected !");
    }

    public override void OnLeftRoom()
    {
        StartCoroutine(WaitToLeave());
    }
    IEnumerator WaitToLeave()
    {
        while (PhotonNetwork.InRoom)
            yield return null;
        isLeavingRoom = false;
        SceneManager.LoadScene("SceneLauncher");
    }

    public void LeaveRoom()
    {
        Debug.Log("Leaving Room");
        isLeavingRoom = true;
        PhotonNetwork.LeaveRoom();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }


}
