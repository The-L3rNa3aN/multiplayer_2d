using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VJ.Networking;

namespace VJ.UI.Game
{
    public class GameUIManager : MonoBehaviour
    {
        public void SendMessage()
        {
            NetworkServer.SendToAll(new Notification { content = "Test" });
        }

        public void Disconnect()
        {
            CustomNetworkManager.instance.DisconnectServer();
        }
    }
}
