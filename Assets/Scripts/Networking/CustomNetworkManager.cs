using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;

namespace VJ.Assets.Scripts.Networking
{
    [RequireComponent(typeof(KcpTransport))]
    public class CustomNetworkManager : NetworkManager
    {
        public static CustomNetworkManager instance;

        public override void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;
        }

        public void ConnectToClient(string ip, string name)
        {
            networkAddress = ip;
            PlayerPrefs.SetString("localPlayerName", name);
            StartClient();
        }

        public void ConnectToServer(string ip, string name)
        {
            networkAddress = ip;
            PlayerPrefs.SetString("localPlayerName", name);
            StartHost();
        }

        /*public override void OnClientConnect()
        {
            base.OnClientConnect();                             //DO NOT DELETE THIS LINE.
            //Debug.Log("Connected to the server...");
            string playerName = PlayerPrefs.GetString("localPlayerName");
            if(!NetworkServer.activeHost)
            {
                NetworkServer.SendToAll(new Notification { content = playerName + " has joined the server." });
            }
        }*/
    }
}