using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Networking;

namespace VJ.Lobby.UI
{
    public class LobbyUIManager : MonoBehaviour
    {
        [Header("UI References")]
        public Button button_ready;
        public Button button_notReady;
        public InputField chatInputField;

        private CustomNetworkManager customNetworkManager;

        private void Start()
        {
            customNetworkManager = CustomNetworkManager.instance;
        }

        public void Ready()
        {
            button_ready.gameObject.SetActive(false);
            button_notReady.gameObject.SetActive(true);
        }

        public void NotReady()
        {
            button_ready.gameObject.SetActive(true);
            button_notReady.gameObject.SetActive(false);
        }

        public void Disconnect()
        {
            //Disconnect.
        }

        public void Send()
        {
            //Send message in chat.
        }
    }
}
