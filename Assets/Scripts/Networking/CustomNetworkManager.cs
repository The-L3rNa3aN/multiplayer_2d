using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager
{
    public static CustomNetworkManager instance;

    public override void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void ConnectToClient(string ip)
    {
        networkAddress = ip;
        StartClient();
    }

    public void ConnectToServer(string ip)
    {
        networkAddress = ip;
        StartServer();
    }
}
