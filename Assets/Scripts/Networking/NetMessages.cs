using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VJ.Networking
{
    public struct Notification : NetworkMessage { public string content; }
    public class NetMessages : MonoBehaviour
    {
        public GameObject notificationText;

        private void Start()
        {
            if (!NetworkClient.active) return;
            NetworkClient.RegisterHandler<Notification>(OnNotification);
        }

        private void OnNotification(Notification msg)
        {
            Text message = Instantiate(notificationText, transform).GetComponent<Text>();
            message.text = msg.content;
        }
    }
}