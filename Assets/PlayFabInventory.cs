using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
public class PlayFabInventory : MonoBehaviour
{
    GetUserInventoryRequest request;
    public GetUserInventoryResult result;
    
    void OnEnable()
    {
        RequestGetPlayerInventory();
    }

    void Update()
    {
    }

    public void RequestGetPlayerInventory()
    {
        request = new GetUserInventoryRequest();                
        

        PlayFabClientAPI.GetUserInventory(request, InventoryResult, OnError);
    }

    void InventoryResult(GetUserInventoryResult result)
    {
        Debug.Log("Inventory result count is " + result.Inventory.Count);        

        this.result = result;
        GetComponent<PlayerInventory>().SetInventoryPlayFab(result);
    }

    void OnError(PlayFabError e)
    {
        Debug.Log(e);
    }
}
