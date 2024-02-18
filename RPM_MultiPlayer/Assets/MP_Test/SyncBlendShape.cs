using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SyncBlendShape : NetworkBehaviour
{
    private SkinnedMeshRenderer player_skinnedMeshRenderer;
    public int blendShapeCount = 67;
    private float[] blendShapeValue = null;
    private bool isRendererReady = false;

    private void OnEnable()
    {
        InitializeBlendShape();
    }

    public void LateUpdate()
    {
        if(!IsRendererReady())
        {
            return;
        }
        UpdateBlendShape();
    }

    public void InitializeBlendShape()
    {
        if(blendShapeCount == null)
        {
            return;
        }

        player_skinnedMeshRenderer = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        Debug.Log(player_skinnedMeshRenderer);
        blendShapeValue = new float[blendShapeCount];
        for(int i = 0; i < blendShapeCount; i++)
            blendShapeValue[i] = 0;
    }

    public bool IsRendererReady()
    {
        if(player_skinnedMeshRenderer != null)
        {
            return true;
        }
        return false;
    }

    private void UpdateBlendShape()
    {
        GetBlendShape();
        SetBlendShape();
        if (IsLocalPlayer)
        {
            if (!IsServer)
            {
                UpdateBlendShapeServerRpc();
            }
            else
            {
                UpdateBlendShapeClientRpc();
            }
        }
    }

    private void GetBlendShape()
    {
        //if(!IsLocalPlayer) return;

        for (int i = 0; i < blendShapeCount; i++)
        {
            blendShapeValue[i] = player_skinnedMeshRenderer.GetBlendShapeWeight(i);
        }
    }

    private void SetBlendShape()
    {
        //if(IsLocalPlayer || IsServer) return;

        for (int i = 0; i < blendShapeCount; i++)
        {
            player_skinnedMeshRenderer.SetBlendShapeWeight(i, blendShapeValue[i]);
        }
    }

    [ServerRpc]
    private void UpdateBlendShapeServerRpc()
    {
        if (!IsLocalPlayer) UpdateBlendShape(); //SetBlendShape();
        UpdateBlendShapeClientRpc();
    }

    [ClientRpc]
    private void UpdateBlendShapeClientRpc()
    {
        if (IsLocalPlayer || IsServer)
        {
            return;
        }
        UpdateBlendShape();
        //SetBlendShape();
    }
}