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

    [Header("Lobby")]
    public GameObject pnLobby;
    public TMP_InputField iRoomName;
    string roomName;

    private void Start()
    {
        pnLobby.SetActive(false);
        pnLogin.SetActive(true);

        if (PlayerPrefs.HasKey("user"))
        {
            iNickName.text = PlayerPrefs.GetString("user");
        }
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
        pnLogin.SetActive(false);
        pnLobby.SetActive(true);

        PlayerPrefs.SetString("user", iNickName.text);
    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
        pnLobby.SetActive(false);
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

        pnLobby.SetActive(false);
        foreach(Player nick in PhotonNetwork.PlayerList)
        {
            print("PlayerList: " + nick.NickName);
        }

        PhotonNetwork.Instantiate(player.name, Vector2.zero, Quaternion.identity);
    }

    public void CreateRoom()
    {
        print("##################### CRIAR SALA ##################");
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(iRoomName.text, opt, TypedLobby.Default, null);
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
