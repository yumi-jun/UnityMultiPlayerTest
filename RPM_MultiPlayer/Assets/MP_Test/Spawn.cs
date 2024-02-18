using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(NetworkObject))]
public class Spawn : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {

        }
    }
}
