using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VJ.Assets.Scripts.Networking;

namespace VJ.Assets.Scripts.UI.Game
{
    public class GameUIManager : MonoBehaviour
    {
        public void SendMessage()
        {
            NetworkServer.SendToAll(new Notification { content = "Test" });
        }
    }
}
