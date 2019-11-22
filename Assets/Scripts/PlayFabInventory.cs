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
    public GameObject toggleButton;
    void OnEnable()
    {
        toggleButton.SetActive(false);
        RequestGetPlayerInventory();
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
        toggleButton.SetActive(true);
    }

    void OnError(PlayFabError e)
    {
        toggleButton.SetActive(true);
        Debug.Log(e);
    }
}
