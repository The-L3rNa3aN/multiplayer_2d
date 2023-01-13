using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;
using VJ.Client;
using VJ.Lobby.UI;

namespace VJ.Networking
{
    [RequireComponent(typeof(KcpTransport))]
    public class CustomNetworkManager : NetworkRoomManager //NetworkManager
    {
        public static CustomNetworkManager instance;
        public int playerCount;

        [Header("Local player related")]
        public bool isPlayerServer;
        public string playerName;

        public override void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;

            networkAddress = GetIP();
        }

        public override void Update()
        {
            base.Update();              //Just in case.

            var players = FindObjectsOfType<Player>();
            playerCount = players.Length;
        }

        public void ConnectToClient(string name, string ip)
        {
            PlayerPrefs.SetString("localPlayerName", name);
            playerName = name;
            networkAddress = ip;
            StartClient();
        }

        public void ConnectToServer(string name, int count)
        {
            PlayerPrefs.SetString("localPlayerName", name);
            maxConnections = count;
            playerName = name;
            StartHost();
        }

        public void DisconnectServer()
        {
            if (isPlayerServer)
                StopServer();
            else
                StopClient();

            networkAddress = GetIP();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            if(playerCount >= maxConnections)
            {
                conn.Disconnect();
                return;
            }
        }

        #region Room callbacks

        public override void OnRoomClientEnter()
        {
            base.OnRoomClientEnter();

            LobbyUIManager.instance.AddNewItem();
        }

        #endregion

        public string GetIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }
    }
}