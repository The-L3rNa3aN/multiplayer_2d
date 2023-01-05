using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public InputField ipAddressField;
    public GameObject mainMenuPanel;
    public GameObject createServerPanel;
    public GameObject joinServerPanel;
    private CustomNetworkManager customNetworkManager;

    private void Start()
    {
        customNetworkManager = CustomNetworkManager.instance;
    }

    public void MainMenu_JoinServer()
    {
        mainMenuPanel.SetActive(false);
        joinServerPanel.SetActive(true);
        ipAddressField.gameObject.SetActive(true);
    }
    
    public void MainMenu_CreateServer()
    {
        mainMenuPanel.SetActive(false);
        createServerPanel.SetActive(true);
        ipAddressField.gameObject.SetActive(true);
    }

    public void Join()
    {
        string ip = ipAddressField.text;
        customNetworkManager.ConnectToClient(ip);
    }

    public void Create()
    {
        string ip = ipAddressField.text;
        customNetworkManager.ConnectToServer(ip);
    }
}
