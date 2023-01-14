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

        [Header("Player List")]
        private MyRoomPlayer[] playerList;
        public Transform playerListContainer;
        public Transform emptyListPool;
        private int itemCount;                  //Number of active items in the list.

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

        private void Update()
        {
            playerList = FindObjectsOfType<MyRoomPlayer>();
            AddItem();
        }

        private void OnGUI()
        {
            for(int i = 0; i < playerListContainer.childCount; i++)
            {
                Transform child = playerListContainer.GetChild(i);
                if(child.GetComponent<PlayerListItem>().linkedRoomPlayer == null)
                {
                    child.SetParent(emptyListPool);
                    child.GetComponent<PlayerListItem>().linkedRoomPlayer = default;
                    child.gameObject.SetActive(false);
                }
            }
        }

        #region Player List Functions
        public void UpdateReadyState() { } //Update ready states of each item in the list.

        public void AddItem()
        {
            PlayerListItem[] listItems = FindObjectsOfType<PlayerListItem>();
            int playerCount = playerList.Length;

            for (int i = 0; i < listItems.Length; i++)
            {
                if (listItems[i].gameObject.activeSelf)
                    itemCount++;
            }

            if (itemCount > playerCount)
                itemCount = playerCount;

            if (itemCount < playerCount)
            {
                Transform newItem = emptyListPool.GetChild(0);
                newItem.SetParent(playerListContainer);
                newItem.GetComponent<PlayerListItem>().SetPlayerName(playerList[playerList.Length - 1]);
                newItem.gameObject.SetActive(true);
            }
        }

        public void RemoveItems()
        {
            //Remove when the player leaves the room.
        }
        #endregion

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
