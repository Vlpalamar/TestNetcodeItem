using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerClickForScore : NetworkBehaviour
{
    private NetworkVariable<int> score= new NetworkVariable<int>(0);
    private Player _player;

    
    public NetworkVariable<int> Score { get => score; }
    public Player Player { get => _player;  }


    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)  
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!_player.Server.IsAllowToChangeScore.Value)
                return;

            AddScore();
            print(score.Value);
        }
           
    }

    public override void OnNetworkSpawn()
    {
        _player = GetComponent<Player>();
        score.OnValueChanged += (int oldData, int newData) => { _player.ScoreTitle.text = _player.PlayerName.Value + ": " + oldData; };
        score.Value = 0;
    }


    public void AddScore()
    {
        if (IsHost)
        {
            Score.Value += 1;
            return;
        }
           
        if (IsClient)
            AddScoreServerRpc();
        
    }

    [ServerRpc]
    public void AddScoreServerRpc()
    {
        Score.Value += 1;
    }
}
