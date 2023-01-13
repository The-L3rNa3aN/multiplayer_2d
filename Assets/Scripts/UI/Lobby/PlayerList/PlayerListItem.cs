using UnityEngine;
using UnityEngine.UI;
using VJ.Client;

namespace VJ
{
    public class PlayerListItem : MonoBehaviour
    {
        public Text UIPlayerName;
        public Text UIReadyState;
        public MyRoomPlayer linkedRoomPlayer = default;

        public void Initialize(string playerName, string readyState)
        {
            UIPlayerName.text = playerName;
            UIReadyState.text = readyState;
        }

        public void SetPlayerName(MyRoomPlayer roomPlayer)
        {
            UIPlayerName.text = roomPlayer.playerName;
            linkedRoomPlayer = roomPlayer;
        }
        public void SetReadyState(string readyState) => UIReadyState.text = readyState;
    }
}
