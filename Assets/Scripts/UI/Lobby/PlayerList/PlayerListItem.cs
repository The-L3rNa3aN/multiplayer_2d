using UnityEngine;
using UnityEngine.UI;
using VJ.Client;
using Mirror;

namespace VJ
{
    public class PlayerListItem : MonoBehaviour
    {
        public Text UIPlayerName;
        public Text UIReadyState;
        public MyRoomPlayer linkedRoomPlayer = default;

        private void OnGUI()
        {
            if (linkedRoomPlayer == null)
                linkedRoomPlayer = default;

            if(linkedRoomPlayer != default || linkedRoomPlayer != null)
                UIPlayerName.text = linkedRoomPlayer.playerName;

            NetworkRoomPlayer nrp = linkedRoomPlayer.GetComponent<NetworkRoomPlayer>();
            if (nrp.readyToBegin)
            {
                UIReadyState.text = "READY";
                UIReadyState.color = Color.green;
            }
            else
            {
                UIReadyState.text = "NOT READY";
                UIReadyState.color = Color.red;
            }
        }

        public void SetPlayerName(MyRoomPlayer roomPlayer) => linkedRoomPlayer = roomPlayer;
    }
}
