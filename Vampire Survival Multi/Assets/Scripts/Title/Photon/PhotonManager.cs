﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 서버 관련 변수
    private string gameVersion = "1.0.0a";

    private TypedLobby lobby = new TypedLobby("lobby1", LobbyType.SqlLobby);

    [Header("참조 스크립트")]
    [SerializeField] private PhotonUI ui;

    private void Awake()
    {
        ui = GetComponent<PhotonUI>();
    }

    /***************************************************************
    * [ 멀티 접속 ]
    * 
    * 매치메이킹 서버 접속
    ***************************************************************/

    public void ConnectServer()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            // 연결 알림창 띄우기
            ui.SetConnectiongPanel(true);

            // 서버 연결
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    public void JoinLobby()
    {
        // 로비 접속
        PhotonNetwork.JoinLobby(lobby);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause != DisconnectCause.ApplicationQuit)
        {
            ui.SetConnectiongPanel(false);
            ui.SetDisconnectedAlert(true);
        }
            
    }

    public override void OnJoinedLobby()
    {
        TypedLobby lobby = PhotonNetwork.CurrentLobby;
        Debug.Log(lobby.Name + "/" + lobby.Type);
        // 연결 알림창 제거
        ui.SetConnectiongPanel(false);
    }
}