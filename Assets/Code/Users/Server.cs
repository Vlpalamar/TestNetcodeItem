using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Server : NetworkBehaviour
{
    [SerializeField] NetworkManagerUI _networkManagerUI;
    [SerializeField] NetworkVariable<bool> isAllowToChangeScore = new NetworkVariable<bool>(true);

    public NetworkVariable<bool> IsAllowToChangeScore { get => isAllowToChangeScore; }

    public void DisAllowToChangeValue()
    {
        isAllowToChangeScore.Value = false;
        DisAllowToChangeValueServerRpc();
    }
    [ServerRpc]
    public void DisAllowToChangeValueServerRpc()
    {
        isAllowToChangeScore.Value = false;
        DisAllowToChangeValueClientRpc();
    }

    [ClientRpc]
    public void DisAllowToChangeValueClientRpc()
    {
        isAllowToChangeScore.Value = false;
    }


}
