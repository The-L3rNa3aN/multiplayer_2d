using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;
using VJ.Client;

namespace VJ.Networking
{
    [RequireComponent(typeof(KcpTransport))]
    public class CustomNetworkManager : NetworkRoomManager //NetworkManager
    {
        public static CustomNetworkManager instance;
        public int playerCount;

        [Header("Local player related")]
        public bool isPlayerServer;

        public override void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;
        }

        public override void Update()
        {
            base.Update();              //Just in case.

            var players = FindObjectsOfType<Player>();
            playerCount = players.Length;
        }

        public void ConnectToClient(string ip, string name)
        {
            networkAddress = ip;
            PlayerPrefs.SetString("localPlayerName", name);
            StartClient();
        }

        public void ConnectToServer(string ip, string name, int count)
        {
            networkAddress = ip;
            PlayerPrefs.SetString("localPlayerName", name);
            maxConnections = count;
            StartHost();
        }

        public void DisconnectServer()
        {
            if (isPlayerServer)
                StopServer();
            else
                StopClient();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            if(playerCount >= maxConnections)
            {
                conn.Disconnect();
                return;
            }
        }
    }
}