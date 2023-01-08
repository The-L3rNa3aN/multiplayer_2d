using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Assets.Scripts.Networking;

namespace VJ.Assets.Scripts.UI.MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [Header("Input Fields")]
        public InputField ipAddressField;
        public InputField nameField;

        [Header("Panels")]
        public GameObject mainMenuPanel;
        public GameObject createServerPanel;
        public GameObject joinServerPanel;

        [Header("Empty Warning Text")]
        public GameObject warningText;

        private CustomNetworkManager customNetworkManager;

        private void Start() => customNetworkManager = CustomNetworkManager.instance;

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

            if(ip != "" && name != "")
                customNetworkManager.ConnectToClient(ip, name);
            else
                StartCoroutine(DislayEmptyWarning());
        }

        public void Create()
        {
            string ip = ipAddressField.text;
            string name = nameField.text;

            if (ip != "" && name != "")
                customNetworkManager.ConnectToServer(ip, name);
            else
                StartCoroutine(DislayEmptyWarning());
        }

        public void Back()
        {
            ipAddressField.gameObject.SetActive(false);
            nameField.gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
            createServerPanel.SetActive(false);
            joinServerPanel.SetActive(false);
        }

        private IEnumerator DislayEmptyWarning()
        {
            warningText.SetActive(true);
            yield return new WaitForSeconds(3f);
            warningText.SetActive(false);
        }
    }
}