using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class MyLauncherScene : MonoBehaviourPunCallbacks
{
    public Button button;
    public TextMeshProUGUI logText;
    private byte maxPlayersPerRoom = 4;

    bool isConnecting;
    string gameVersion = "1.0";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        logText.text = "";
        isConnecting = true;
        button.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }

    void LogFeedback(string message)
    {
        if (logText == null)
        {
            return;
        }
        logText.text += System.Environment.NewLine + message;

    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
            Debug.Log("OnConnectedToMaster() was called.");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogFeedback("OnJoinRandomFailed: Next -> Create a new Room");
        Debug.Log("OnJoinRandomFailed() was called.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("OnDisconnected" + cause);
        Debug.LogError("Disconnected");
        isConnecting = false;
        button.interactable = false;
    }
    public override void OnJoinedRoom()
    {
        LogFeedback("OnJoinedRoom with " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("OnJoinedRoom() was called.");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");
            PhotonNetwork.LoadLevel("SampleScene");
        }


    }
}