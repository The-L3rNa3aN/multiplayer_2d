using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerTag : NetworkBehaviour
{
    public string playerName;
    public TextMeshPro nameplate;

    private void Start()
    {
        if (!isLocalPlayer) return;
        playerName = PlayerPrefs.GetString("localPlayerName");
        CmdSetName(playerName);
        nameplate.text = playerName;
    }
    [Command] public void CmdSetName(string n) => RpcSetName(n);
    [ClientRpc] public void RpcSetName(string n) => playerName = n;
}
