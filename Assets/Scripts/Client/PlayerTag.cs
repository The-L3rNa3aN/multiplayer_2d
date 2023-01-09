using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

namespace VJ.Client
{
    public class PlayerTag : NetworkBehaviour
    {
        public TextMeshPro nameplate;
        public string playerName;
        public int score;

        private void Start()
        {
            if (!isLocalPlayer) return;
            playerName = PlayerPrefs.GetString("localPlayerName");
        }

        private void Update()
        {
            CmdSetName(playerName);
            nameplate.text = playerName;
        }

        [Command] public void CmdSetName(string n) => RpcSetName(n);
        [ClientRpc] public void RpcSetName(string n) => playerName = n;
    }
}