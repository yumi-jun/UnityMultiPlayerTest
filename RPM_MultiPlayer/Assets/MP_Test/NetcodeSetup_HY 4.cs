using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace ReadyPlayerMe.NetcodeSupport
{
    public class NetcodeSetup_HY4 : MonoBehaviour
    {
        [SerializeField] private Button hostButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private string gameplaySceneName = "Gameplay";
        
        private void Start()
        {
            hostButton.onClick.AddListener(() =>
            {
                Debug.Log("start host");
                NetworkManager.Singleton.StartHost();
                NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
            });
            
            clientButton.onClick.AddListener(() =>
            {
                Debug.Log("start client");
                NetworkManager.Singleton.StartClient();
            });
            
        }
    }
}

