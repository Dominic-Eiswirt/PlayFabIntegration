using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.AuthenticationModels;
using PlayFab.DataModels;
public class PlayfabEntity : MonoBehaviour
{
    public bool getEntity;
    void Start()
    {
        // if (!getEntity)
        // {
        //setEntity
        PlayFabAuthenticationAPI.GetEntityToken(new GetEntityTokenRequest(), OnEntitySuccess, OnEntityFailure);
        //  }
        //  else
        //  {

        //   }
    }

    private void OnEntitySuccess(GetEntityTokenResponse responseResult)
    {
        PlayerEntity.id = responseResult.Entity.Id;
        PlayerEntity.type = responseResult.Entity.Type;
        SetTestEntityOnPlayer();
    }

    private void OnEntityFailure(PlayFabError error)
    {
        Debug.LogError(error);
        Debug.LogError(error.ErrorMessage);
        Debug.LogError(error.ErrorDetails);
    }

    private void SetTestEntityOnPlayer()
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "Health", 100 },
            { "Stamina", 100 },
        };

        List<SetObject> dataList = new List<SetObject>()
        {
            new SetObject()
            {
                ObjectName = "TestPlayerData",
                DataObject = data
            },
        };

        PlayFabDataAPI.SetObjects(new SetObjectsRequest()
        {
            Entity = new PlayFab.DataModels.EntityKey { Id = PlayerEntity.id, Type = PlayerEntity.type },
            Objects = dataList,
        }, (setResult) =>
        {
            Debug.Log(setResult.ProfileVersion);
        },
        (error) =>
        {
            Debug.Log(error.ErrorDetails);
        });

        Debug.Log("|||||||||||||||||||||||||||||||||||||||||||||||");
        RequestEntityInfo();
    }

    private void RequestEntityInfo()
    {
        GetObjectsRequest request = new GetObjectsRequest
        {
            Entity = new PlayFab.DataModels.EntityKey
            {
                Id = PlayerEntity.id,
                Type = PlayerEntity.type
            }
        };
        PlayFabDataAPI.GetObjects(
            request,
            result =>
            {
                Debug.Log(result.Objects["TestPlayerData"].ToJson());
            },
            (error) => Debug.Log(error));
            

    }
}
