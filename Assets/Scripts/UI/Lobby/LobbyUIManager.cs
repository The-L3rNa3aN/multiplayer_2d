using JetBrains.Annotations;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Client;
using VJ.Networking;
using static UnityEditor.Progress;

namespace VJ.Lobby.UI
{
    public class LobbyUIManager : MonoBehaviour
    {
        public static LobbyUIManager instance;

        [Header("UI References")]
        public Button button_ready;
        public Button button_notReady;

        [Header("Player List")]
        public GameObject playerListItem;       //Prefab.
        private MyRoomPlayer[] playerList;
        public Transform playerListContainer;
        private int n;
        [HideInInspector] public PlayerListItem[] listItems;            //For later.

        [Header("Chat")]
        public InputField chatInputField;
        public GameObject chatMessage;          //Prefab.
        public Transform chatContainer;

        private CustomNetworkManager customNetworkManager;
        [HideInInspector] public NetworkRoomPlayer localPlayer;

        private void Awake()
        {
            //Singleton.
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;
        }

        private void Start()
        {
            customNetworkManager = CustomNetworkManager.instance;
        }

        private void OnGUI()
        {
            playerList = FindObjectsOfType<MyRoomPlayer>();
            int count = playerList.Length;

            foreach (MyRoomPlayer player in playerList)                                     //Not optimal at all. I need to find another way to have this updated at all times.
            {
                NetworkRoomPlayer roomPlayer = player.GetComponent<NetworkRoomPlayer>();
                if(n < count)
                {
                    n++;
                    PlayerListItem item = Instantiate(playerListItem, playerListContainer).GetComponent<PlayerListItem>();
                    string pName = player.playerName;
                    string rState = "";
                    if (roomPlayer.readyToBegin)
                        rState = "READY";
                    else
                        rState = "NOT READY";

                    item.Initialize(pName, rState);
                }
            }
        }

        public void UpdatePlayerList()
        {
            //Assuming I'm not using prefabs and I'm using a pre-existing pool of empty and inactive objects.

            for(int i = 0; i < playerList.Length; i++)
            {
                NetworkRoomPlayer roomPlayer = playerList[i].GetComponent<NetworkRoomPlayer>();
                string pName = playerList[i].playerName;
                string rState = "";
                if (roomPlayer.readyToBegin)
                    rState = "READY";
                else
                    rState = "NOT READY";

                listItems[i].gameObject.SetActive(true);                                        //Because they are initially inactive.
                listItems[i].transform.SetParent(chatContainer, false);
                listItems[i].Initialize(pName, rState);
            }
        }

        public void ChangeReadyState()
        {
            if(!localPlayer.readyToBegin)
            {
                localPlayer.readyToBegin = true;
                button_ready.gameObject.SetActive(false);
                button_notReady.gameObject.SetActive(true);
            }
            else
            {
                localPlayer.readyToBegin = false;
                button_ready.gameObject.SetActive(true);
                button_notReady.gameObject.SetActive(false);
            }
        }

        public void Disconnect()
        {
            //Disconnect.
        }

        public void Send()
        {
            //Send message in chat.
            if(chatInputField.text != null || chatInputField.text != "")
            {
                string chatText = chatInputField.text;
                string playerName = customNetworkManager.playerName;

                chatInputField.text = "";
                //chatMessage = Instantiate(chatMessage, chatContainer);
                NetworkServer.SendToAll(new Notification { content = playerName + ": " + chatText }); //chatMessage.GetComponent<Text>().text = playerName + ": " + chatText;

                chatInputField.ActivateInputField();
            }
        }
    }
}
