using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private float _moveSpeed = 2;

    public float MoveSpeed { get => _moveSpeed; }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        Vector3 moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveDir.y += 1f;
        if (Input.GetKey(KeyCode.S)) moveDir.y -= 1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x += 1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x -= 1f;

        if (IsServer && IsLocalPlayer)
            Move(moveDir);
        if (!IsServer && IsLocalPlayer)
            MoveServerRpc(moveDir);
        
            
       
    }

    void Move(Vector3 moveDir)
    {
        this.transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    void MoveServerRpc(Vector3 moveDir)
    {
        Move(moveDir);
    }

   
}


