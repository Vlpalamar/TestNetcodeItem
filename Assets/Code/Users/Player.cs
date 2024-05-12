using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(NetworkTransform))] 
[RequireComponent(typeof(NetworkObject))]
[RequireComponent (typeof(PlayerClickForScore))]
public class Player : NetworkBehaviour
{

    
    [SerializeField]
    private TMP_Text scoreTitle;
    [SerializeField]
    private NetworkVariable<FixedString64Bytes> _playerName = new NetworkVariable<FixedString64Bytes>(""); //имя должно получаться с базы данных, но для такого маленького проэкта это не нужно
    private PlayerMovement _playerMovement;
    private PlayerClickForScore _playerClickForScore;
    private Server _server;

    public PlayerMovement PlayerMovement { get => _playerMovement; }
    public NetworkVariable<FixedString64Bytes> PlayerName { get => _playerName; }
    public PlayerClickForScore PlayerClickForScore { get => _playerClickForScore;  }

    public TMP_Text ScoreTitle { get => scoreTitle; }
    public Server Server { get => _server;  }

    public override void OnNetworkSpawn()
    {
        if (!IsLocalPlayer) return;
        _server = FindFirstObjectByType<Server>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerClickForScore = GetComponent<PlayerClickForScore>();
        GetNameServerRpc();
    }

    [ServerRpc]
    private void GetNameServerRpc()
    {
        _playerName.Value = "Player " + OwnerClientId.ToString();
    }



   
    
}
