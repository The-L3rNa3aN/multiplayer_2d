using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager
{
    public InputField ipAddress;
    private NetworkManager networkManager;

    public override void Start()
    {
        networkManager= GetComponent<NetworkManager>();
    }
    public void ConnectToClient()
    {
        networkManager.networkAddress = ipAddress.text;
        networkManager.StartClient();
    }
}
