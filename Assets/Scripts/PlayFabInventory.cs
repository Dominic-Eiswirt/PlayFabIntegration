using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
public class PlayFabInventory : MonoBehaviour
{
    public GetUserInventoryResult result;
    public GameObject toggleButton;
    private GetUserInventoryRequest request;

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

    private void InventoryResult(GetUserInventoryResult result)
    {
        this.result = result;
        GetComponent<PlayerInventory>().SetInventoryPlayFab(result);
        toggleButton.SetActive(true);
    }

    private void OnError(PlayFabError e)
    {
        toggleButton.SetActive(true);
        Debug.Log(e);
    }
}
