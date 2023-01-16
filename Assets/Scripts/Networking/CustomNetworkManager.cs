using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using kcp2k;
using VJ.Client;
using VJ.Lobby.UI;

namespace VJ.Networking
{
    [RequireComponent(typeof(KcpTransport))]
    public class CustomNetworkManager : NetworkRoomManager
    {
        public static CustomNetworkManager instance;
        public int playerCount;

        [Header("Local player related")]
        public bool isPlayerServer;
        public string playerName;

        [Header("Match Start variables")]
        public int timer = 0;
        private int duration = 4;

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

            if(SceneManager.GetActiveScene().name == "OnlineScene")
            {
                var players = FindObjectsOfType<Player>();
                playerCount = players.Length;
            }
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
                StopHost();
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

        public string GetIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        private IEnumerator CoMatchStart()
        {
            float time = 0f;
            while(timer <= duration)
            {
                time += Time.deltaTime; // / duration;
                timer = (int)time;
                yield return null;
            }
            ServerChangeScene(GameplayScene);
        }

        #region Room Callbacks

        public override void OnRoomServerPlayersReady()
        {
            Debug.Log("All players are ready. Loading level...");
            LobbyUIManager.instance.timerDisplay.gameObject.SetActive(true);
            StartCoroutine(CoMatchStart());
            //ServerChangeScene(GameplayScene);
        }

        #endregion
    }
}