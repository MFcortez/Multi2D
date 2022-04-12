using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class NetworkController2 : MonoBehaviourPunCallbacks
{
    public GameObject player;

    private void Start()
    {
        Login();
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
    }

    // ================================================
    // PUN callbacks
    // ================================================

    public override void OnConnectedToMaster()
    {
        print("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRandomFailed: " + message);
        PhotonNetwork.CreateRoom("Facens");
    }

    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");

        print("Nome da sala: " + PhotonNetwork.CurrentRoom.Name);
        print("Players conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        //pnLobby.SetActive(false);
        foreach (Player nick in PhotonNetwork.PlayerList)
        {
            print("PlayerList: " + nick.NickName);
        }

        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}