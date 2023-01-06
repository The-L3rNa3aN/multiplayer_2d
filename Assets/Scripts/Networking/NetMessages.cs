using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Notification : NetworkMessage { public string content; }
public class NetMessages : MonoBehaviour
{
    [SerializeField] private Text notificationText = null;

    private void Start()
    {
        if (!NetworkClient.active) return;

        NetworkClient.RegisterHandler<Notification>(OnNotification);
    }

    private void OnNotification(Notification msg)
    {

    }
}
