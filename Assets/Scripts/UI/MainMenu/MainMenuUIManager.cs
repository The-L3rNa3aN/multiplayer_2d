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
        public InputField nameField;
        public InputField maxPlayersField;
        public InputField ipField;

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
            nameField.gameObject.SetActive(true);
            ipField.gameObject.SetActive(true);
        }

        public void MainMenu_CreateServer()
        {
            mainMenuPanel.SetActive(false);
            createServerPanel.SetActive(true);
            nameField.gameObject.SetActive(true);
            maxPlayersField.gameObject.SetActive(true);
        }

        public void Join()
        {
            string name = nameField.text;
            string ip = ipField.text;

            if (name != "")
                customNetworkManager.ConnectToClient(name, ip);
            else
                StartCoroutine(DislayEmptyWarning());
        }

        public void Create()
        {
            string name = nameField.text;
            int count = Int32.Parse(maxPlayersField.text);

            if (name != "")
            {
                if (count > 1 && count <= 20)
                    customNetworkManager.ConnectToServer(name, count);
                else
                    StartCoroutine(DisplayMaxPlayerCountWarning());
            }
            else
                StartCoroutine(DislayEmptyWarning());

            CustomNetworkManager.instance.isPlayerServer = true;
        }

        public void Back()
        {
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