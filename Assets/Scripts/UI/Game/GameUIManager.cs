using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using VJ.Networking;
using VJ.Client;

namespace VJ.UI.Game
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Pause UI")]
        public GameObject pausePanel;
        public bool isPaused;

        [Header("Connection info")]
        public Text connInfo;

        [Header("Toggle scoreboard")]
        public GameObject scoreboardPanel;
        public GameObject heading_name;
        public GameObject heading_score;
        public GameObject scoreboardItem;       //Prefab.

        private void Start()
        {
            string netAdd = CustomNetworkManager.instance.networkAddress;
            connInfo.text = "Connected to " + netAdd;
        }

        private void Update()
        {
            Scoreboard();

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

        private void Scoreboard()
        {
            PlayerTag[] players = FindObjectsOfType<PlayerTag>();

            if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
            {
                scoreboardPanel.SetActive(true);
                heading_name.SetActive(true);
                heading_score.SetActive(true);
                foreach (PlayerTag player in players)
                {
                    ScoreboardItem item = Instantiate(scoreboardItem, scoreboardPanel.transform).GetComponent<ScoreboardItem>();
                    item.InitializeItem(player);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                scoreboardPanel.SetActive(false);
                heading_name.SetActive(false);
                heading_score.SetActive(false);
                var items = scoreboardPanel.GetComponentsInChildren<ScoreboardItem>();
                foreach (var item in items)
                    Destroy(item.gameObject);
            }
        }
    }
}
