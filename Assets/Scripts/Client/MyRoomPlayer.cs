using UnityEngine;
using Mirror;
using UnityEngine.UI;
using VJ.Networking;

namespace VJ.Client
{
    public class MyRoomPlayer : NetworkBehaviour
    {
        public Text playerName;
        public Text readyState;

        private CustomNetworkManager customNetworkManager;
        private NetworkRoomPlayer nrp;

        private void Start()
        {
            customNetworkManager = CustomNetworkManager.instance;
            nrp = GetComponent<NetworkRoomPlayer>();

            GameObject par = GameObject.FindGameObjectWithTag("PlayerListAndState");
            transform.SetParent(par.transform, false);
            transform.position = new Vector3(0, 0, 0);

            CmdGUISetName();
        }

        private void OnGUI() => CmdGUISetPlayerState(nrp.readyToBegin);

        [Command] private void CmdGUISetName()
        {
            if(isLocalPlayer)
                playerName.text = customNetworkManager.playerName;
        }

        [Command] private void CmdGUISetPlayerState(bool state)
        {
            if(state)
            {
                readyState.text = "READY";
                readyState.color = Color.green;
            }
            else
            {
                readyState.text = "NOT READY";
                readyState.color = Color.red;
            }
        }
    }
}
