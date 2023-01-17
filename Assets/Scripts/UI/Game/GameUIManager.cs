using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using VJ.Networking;

namespace VJ.UI.Game
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Pause UI")]
        public GameObject pausePanel;
        public bool isPaused;

        [Header("Connection info")]
        public Text connInfo;

        private void Start()
        {
            string netAdd = CustomNetworkManager.instance.networkAddress;
            connInfo.text = "Connected to " + netAdd;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                isPaused = isPaused ? isPaused = false : isPaused = true;
        }

        private void OnGUI()
        {
            if (isPaused)
                pausePanel.SetActive(true);
            else
                pausePanel.SetActive(false);
        }

        public void SendMessage() => NetworkServer.SendToAll(new Notification { content = "Test" });

        public void Disconnect() => CustomNetworkManager.instance.DisconnectServer();
    }
}
