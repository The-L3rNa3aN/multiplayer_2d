using UnityEngine;
using Mirror;
using UnityEngine.UI;
using VJ.Networking;
using VJ.Lobby.UI;

namespace VJ.Client
{
    public class MyRoomPlayer : NetworkBehaviour
    {
        [SyncVar] public string playerName;
        //[SyncVar] public string readyState;

        private void Start()
        {
            if (!isLocalPlayer) return;

            playerName = CustomNetworkManager.instance.playerName;
            LobbyUIManager.instance.localPlayer = GetComponent<NetworkRoomPlayer>();
        }
    }
}
