using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Networking;

namespace VJ
{
    public class CustomRoomPlayer : NetworkRoomPlayer
    {
        [Header("Custom stuff")]
        public Text displayName;
        public Text readyState;

        public new void Start()
        {
            if(isLocalPlayer)
                displayName.text = CustomNetworkManager.instance.playerName;
        }
    }
}
