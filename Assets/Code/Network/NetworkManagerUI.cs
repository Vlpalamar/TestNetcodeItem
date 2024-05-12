using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : NetworkBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;
    [SerializeField] private Button DisAllowToChangeScoreBtn;

    [Header("Server")]
    [SerializeField] private Server _server;
    

    private void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            HostBtn.GetComponent<Transform>().gameObject.SetActive(false);
            ClientBtn.GetComponent<Transform>().gameObject.SetActive(false);
            DisAllowToChangeScoreBtn.GetComponent<Transform>().gameObject.SetActive(true);


        });
        HostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            serverBtn.GetComponent<Transform>().gameObject.SetActive(false);
            ClientBtn.GetComponent<Transform>().gameObject.SetActive(false);
            DisAllowToChangeScoreBtn.GetComponent<Transform>().gameObject.SetActive(true);

        });
        ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            HostBtn.GetComponent<Transform>().gameObject.SetActive(false);
            serverBtn.GetComponent<Transform>().gameObject.SetActive(false);
        });

        DisAllowToChangeScoreBtn.onClick.AddListener(() =>
        {
            if (IsServer || IsHost)
            {
                _server.DisAllowToChangeValue();
            }
        });
    }


   

    


}
