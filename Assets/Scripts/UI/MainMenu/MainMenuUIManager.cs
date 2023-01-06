using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Input Fields")]
    public InputField ipAddressField;
    public InputField nameField;

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
        nameField.gameObject.SetActive(true);
    }
    
    public void MainMenu_CreateServer()
    {
        mainMenuPanel.SetActive(false);
        createServerPanel.SetActive(true);
        ipAddressField.gameObject.SetActive(true);
        nameField.gameObject.SetActive(true);
    }

    public void Join()
    {
        string ip = ipAddressField.text;
        string name = nameField.text;
        customNetworkManager.ConnectToClient(ip, name);
    }

    public void Create()
    {
        string ip = ipAddressField.text;
        string name = nameField.text;
        customNetworkManager.ConnectToServer(ip, name);
    }

    public void Back()
    {
        ipAddressField.gameObject.SetActive(false);
        nameField.gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
        createServerPanel.SetActive(false);
        joinServerPanel.SetActive(false);
    }
}
