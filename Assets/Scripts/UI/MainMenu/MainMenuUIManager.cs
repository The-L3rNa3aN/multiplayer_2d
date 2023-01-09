using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Networking;

namespace VJ.UI.MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [Header("Input Fields")]
        public InputField ipAddressField;
        public InputField nameField;
        public InputField maxPlayersField;

        [Header("Panels")]
        public GameObject mainMenuPanel;
        public GameObject createServerPanel;
        public GameObject joinServerPanel;

        [Header("Warning Text")]
        public GameObject warningText;
        public GameObject mPlayerWarningText;

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
            maxPlayersField.gameObject.SetActive(true);
        }

        public void Join()
        {
            string ip = ipAddressField.text;
            string name = nameField.text;

            if (ip != "" && name != "")
                customNetworkManager.ConnectToClient(ip, name);
            else
                StartCoroutine(DislayEmptyWarning());
        }

        public void Create()
        {
            string ip = ipAddressField.text;
            string name = nameField.text;
            int count = Int32.Parse(maxPlayersField.text);

            if (ip != "" && name != "")
            {
                if (count > 1 && count <= 20)
                    customNetworkManager.ConnectToServer(ip, name, count);
                else
                    StartCoroutine(DisplayMaxPlayerCountWarning());
            }
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

        private IEnumerator DisplayMaxPlayerCountWarning()
        {
            mPlayerWarningText.SetActive(true);
            yield return new WaitForSeconds(3f);
            mPlayerWarningText.SetActive(false);
        }
    }
}