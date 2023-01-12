using UnityEngine;
using UnityEngine.UI;

namespace VJ
{
    public class PlayerListItem : MonoBehaviour
    {
        public Text UIPlayerName;
        public Text UIReadyState;
        public void Initialize(string playerName, string readyState)
        {
            UIPlayerName.text = playerName;
            UIReadyState.text = readyState;
        }
    }
}
