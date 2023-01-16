using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Client;
using VJ.Networking;

namespace VJ.Lobby.UI
{
    public class LobbyUIManager : MonoBehaviour
    {
        public static LobbyUIManager instance;

        [Header("UI References")]
        public Button button_ready;
        public Button button_notReady;
        public Text timerDisplay;

        [Header("Player List")]
        [SerializeField] private MyRoomPlayer[] playerList;
        public Transform playerListContainer;
        public Transform emptyListPool;
        [SerializeField] private int itemCount;                  //Number of active items in the list.
        [SerializeField] private int playerCount;                //Number of players in the server.

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

        private void Start() => customNetworkManager = CustomNetworkManager.instance;

        private void OnGUI()
        {
            string t = customNetworkManager.timer.ToString();
            timerDisplay.text = "Match begins in + " + t + "...";
        }

        private void Update()
        {
            playerList = FindObjectsOfType<MyRoomPlayer>();
            playerCount = playerList.Length;
            itemCount = playerListContainer.childCount;

            AddItem();
            RemoveItems();
        }

        #region Player List Functions
        public void AddItem()                                   //Adds items when there are new players in the server.
        {
            if (itemCount > playerCount)
                itemCount = playerCount;

            for (int i = itemCount; i < playerCount; i++)
            {
                Transform newItem = emptyListPool.GetChild(0);  //The first object in the pool.
                newItem.SetParent(playerListContainer);
                newItem.gameObject.SetActive(true);
                newItem.GetComponent<PlayerListItem>().SetPlayerName(playerList[i]);        //This doesn't work for players who are already in the server.
                //i++;
            }
        }

        public void RemoveItems()                               //Removes items when players leave the server.
        {
            for (int i = 0; i < playerListContainer.childCount; i++)
            {
                Transform child = playerListContainer.GetChild(i);
                if (child.GetComponent<PlayerListItem>().linkedRoomPlayer == null)
                {
                    child.SetParent(emptyListPool);
                    child.GetComponent<PlayerListItem>().linkedRoomPlayer = default;
                    child.gameObject.SetActive(false);
                }
            }
        }
        #endregion

        public void ChangeReadyState()
        {
            if(!localPlayer.readyToBegin)
            {
                localPlayer.GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(true); //localPlayer.readyToBegin = true;
                button_ready.gameObject.SetActive(false);
                button_notReady.gameObject.SetActive(true);
            }
            else
            {
                localPlayer.GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(false); //localPlayer.readyToBegin = false;
                button_ready.gameObject.SetActive(true);
                button_notReady.gameObject.SetActive(false);
            }
        }

        public void Disconnect() => customNetworkManager.DisconnectServer();

        public void Send()                                      //Send message in chat.
        {
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
