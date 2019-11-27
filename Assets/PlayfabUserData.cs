using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
/// <summary>
/// For User data
/// </summary>
public class PlayfabUserData : MonoBehaviour
{
    public delegate void NoPlayerInfoAvailableUserData();
    public delegate void UserDataEvents(Dictionary<string, string> dict);
    public event NoPlayerInfoAvailableUserData OnPlayerIsNewAndHasNoInfo;
    public event UserDataEvents OnDataReceived;    
    public static PlayfabUserData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GetUserData()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {            
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = PlayFabLogin.thisPlayerId,
                Keys = null
            }, OnUserDataRequestSuccess, OnUserDataRequestFailure);
        }
    }

    private void OnUserDataRequestSuccess(GetUserDataResult result)
    {
        if(result.Data == null || !result.Data.ContainsKey("Weapon") && !result.Data.ContainsKey("Kills"))
        {
            Debug.Log("Some thing is not right");
            OnPlayerIsNewAndHasNoInfo?.Invoke();
        }
        else
        {   
            Dictionary<string, string> newData = new Dictionary<string, string>();
            newData.Add("Weapon", result.Data["Weapon"].Value);
            newData.Add("Kills", result.Data["Kills"].Value);
            OnDataReceived?.Invoke(newData);
            Debug.Log("Weapon: " + result.Data["Weapon"].Value);
            Debug.Log("Kills: " + result.Data["Kills"].Value);
        }
    }

    private void OnUserDataRequestFailure(PlayFabError error)
    {
        Debug.Log(error);        
    }

    public void SetUserData(Dictionary<string,string> data)
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = data

            }, OnSetUserDataSuccess, OnSetUserDataFailure);
        }
    }

    private void OnSetUserDataSuccess(UpdateUserDataResult result)
    {
        //This gets called on gamemanager end game. So we immediately call get user data so we receive the info in the lobby
        Debug.Log("Successfully set user data");
        GetUserData();
    }
    private void OnSetUserDataFailure(PlayFabError error)
    {
        Debug.Log(error);
    }
}
