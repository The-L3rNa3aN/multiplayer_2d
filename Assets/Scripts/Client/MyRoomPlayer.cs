using UnityEngine;
using Mirror;
using VJ.Networking;
using VJ.Lobby.UI;

namespace VJ.Client
{
    public class MyRoomPlayer : NetworkBehaviour
    {
        [SyncVar] public string playerName;
        private NetworkRoomPlayer nrp;

        private void Start()
        {
            if (!isLocalPlayer) return;

            CmdSetName();
            nrp = GetComponent<NetworkRoomPlayer>();
            LobbyUIManager lobbyManager = LobbyUIManager.instance;
            lobbyManager.localPlayer = nrp;
        }

        [Command] public void CmdSetName() => RpcSetName();
        [ClientRpc] public void RpcSetName() => playerName = PlayerPrefs.GetString("localPlayerName");
    }
}
