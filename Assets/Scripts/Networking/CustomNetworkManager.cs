using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;
using VJ.Client;
using UnityEngine.SceneManagement;

namespace VJ.Networking
{
    [RequireComponent(typeof(KcpTransport))]
    public class CustomNetworkManager : NetworkManager
    {
        public static CustomNetworkManager instance;
        public int playerCount;

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

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            //base.OnServerConnect(conn);
            if(playerCount >= maxConnections)
            {
                conn.Disconnect();
                return;
            }
        }

        /*public override void OnClientConnect()
        {
            base.OnClientConnect();
            string playerName = PlayerPrefs.GetString("localPlayerName");
            NetworkServer.SendToAll(new TestNotification { content = playerName + " has joined." });
        }*/
    }
}