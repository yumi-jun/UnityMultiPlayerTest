using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterDatabase characterDatabase;

    [SerializeField] private Transform point;

    private Vector3 spawnPos;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in ServerManager.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            Debug.Log(client.Key);
            if (character != null)
            {
                // when the client is host
                if (IsHost)
                {
                    spawnPos = point.position;
                }
                // whetn the client is client
                else
                {
                   spawnPos = new Vector3(-4f, -1.7f, 2.8f);
                }

                var characterInstance = Instantiate(character.GamePlayerPrefab, spawnPos, Quaternion.identity);
                characterInstance.SpawnAsPlayerObject(client.Value.ClientId);
            }
        }
    }
    
}
