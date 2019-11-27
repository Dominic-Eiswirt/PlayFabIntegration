using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
/// <summary>
/// For User data
/// </summary>
public class PlayfabUserData : MonoBehaviour
{

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F6))
        {
            GetUserData();
        }        
    }

    private void GetUserData()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = PlayerEntity.id,
                Keys = null
            }, OnUserDataRequestSuccess, OnUserDataRequestFailure);
        }
    }

    private void OnUserDataRequestSuccess(GetUserDataResult result)
    {
        if(result.Data == null || !result.Data.ContainsKey("Weapon") && !result.Data.ContainsKey("Kills"))
        {
            Debug.Log("Some thing is not right");
        }
        else
        {
            Debug.Log("Weapon: " + result.Data["Weapon"]);
            Debug.Log("Kills: " + result.Data["Kills"]);
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
        Debug.Log("Successfully set user data");
    }
    private void OnSetUserDataFailure(PlayFabError error)
    {
        Debug.Log(error);
    }
}
