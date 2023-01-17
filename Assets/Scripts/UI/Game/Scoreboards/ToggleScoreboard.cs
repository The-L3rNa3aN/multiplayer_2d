using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VJ.Client;

namespace VJ.UI.Game.Scoreboards
{
    public class ToggleScoreboard : MonoBehaviour
    {
        public GameObject scoreboardPanel;
        public GameObject heading_name;
        public GameObject heading_score;
        public GameObject scoreboardItem;       //Prefab.

        public GameUIManager gameUIManager;

        private void Start()
        {
            gameUIManager.GetComponent<GameUIManager>();
        }

        private void Update()
        {
            PlayerTag[] players = FindObjectsOfType<PlayerTag>();
            
            if (Input.GetKeyDown(KeyCode.Tab))
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
            else //if (Input.GetKeyUp(KeyCode.Tab))
            {
                scoreboardPanel.SetActive(false);
                heading_name.SetActive(false);
                heading_score.SetActive(false);
                var items = scoreboardPanel.GetComponentsInChildren<ScoreboardItem>();
                foreach (var item in items)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}