using UnityEngine;
using Mirror;
using VJ.Networking;
using VJ.Lobby.UI;

namespace VJ.Client
{
    public class MyRoomPlayer : NetworkBehaviour
    {
        [SyncVar] public string playerName;

        private void Start()
        {
            if (!isLocalPlayer) return;

            CmdSetName();
            LobbyUIManager lobbyManager = LobbyUIManager.instance;
            lobbyManager.localPlayer = GetComponent<NetworkRoomPlayer>();
        }

        [Command] public void CmdSetName() => RpcSetName();
        [ClientRpc] public void RpcSetName() => playerName = PlayerPrefs.GetString("localPlayerName");
    }
}
