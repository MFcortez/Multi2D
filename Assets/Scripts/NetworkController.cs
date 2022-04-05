using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public GameObject player;

    [Header("LOGIN")]
    public GameObject pnLogin;
    public TMP_InputField iNickName;
    public Button btnLogin;
    string nickname;

    private void Start()
    {
        pnLogin.SetActive(true);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        PhotonNetwork.NickName = "FRC";
    }

    // ================================================
    // HELPERS
    // ================================================
    public void Login()
    {
        print("##################### LOGIN ##################");
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        btnLogin.interactable = false;
    }

    public void JoinRandomRoom()
    {
        print("##################### BUSCAR PARTIDA ##################");
        PhotonNetwork.JoinLobby();
    }

    // ================================================
    // PUN callbacks
    // ================================================
    public override void OnConnected()
    {
        print("OnConnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("OnDisconnected: " + cause);
    }

    public override void OnConnectedToMaster()
    {
        print("Conectado fi!");
        PhotonNetwork.NickName = iNickName.text;
    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRandomFailed: "+message);
        PhotonNetwork.CreateRoom("Facens");
    }

    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");
        
        print("Nome da sala: "+ PhotonNetwork.CurrentRoom.Name);
        print("Players conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        //pnLobby.SetActive(false);
        foreach(Player nick in PhotonNetwork.PlayerList)
        {
            print("PlayerList: " + nick.NickName);
        }

        PhotonNetwork.Instantiate(player.name, Vector2.zero, Quaternion.identity);
    }

    public override void OnCreatedRoom()
    {
        print("OnCreatedRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRoomFailed: "+message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnCreateRoomFailed: " + message);
    }
}
