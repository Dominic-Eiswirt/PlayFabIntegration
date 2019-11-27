using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.AuthenticationModels;
using PlayFab.DataModels;
public class PlayfabEntity : MonoBehaviour
{
    public static PlayfabEntity instance;
    public delegate void EntityEvents(ObjectResult s);
    public delegate void NoPlayerInfo();
    public event NoPlayerInfo OnPlayerIsNewAndHasNoData;
    public event EntityEvents OnReceivedLastWeapon;
    public bool getEntity;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UICenter.instance.OnStateChange += GetEntityToken;
    }
    private void OnDisable()
    {
        UICenter.instance.OnStateChange -=  GetEntityToken;
    }

    public void GetEntityToken()
    {
        if (PlayFab.PlayFabClientAPI.IsClientLoggedIn() && PlayerEntity.type == "" && PlayerEntity.id == "")
        {
            Debug.Log("Player is logged in and has no ID or Type, running entity token to fetch ID and type");
            PlayFabAuthenticationAPI.GetEntityToken(new GetEntityTokenRequest(), OnEntitySuccess, OnEntityFailure);
        }
        else
        {
            Debug.Log(string.Format("Not doing anything. Player logged in status is {0}. Player id is: {1}. Player type is: {2}",
                                                                                    PlayFabClientAPI.IsClientLoggedIn().ToString(),                                                           PlayerEntity.id, 
                                                                                            PlayerEntity.type));
        }
    }

    private void OnEntitySuccess(GetEntityTokenResponse responseResult)
    {
        PlayerEntity.id = responseResult.Entity.Id;
        PlayerEntity.type = responseResult.Entity.Type;        
    }

    private void OnEntityFailure(PlayFabError error)
    {
        Debug.LogError(error);
        Debug.LogError(error.ErrorMessage);
        Debug.LogError(error.ErrorDetails);
    }

    public void SetWeaponBeforeGameEnd(string jsonString)
    {
      //This gets called on gamemanager end game. So we immediately call get user data so we receive the info in the lobby
        List<SetObject> dataList = new List<SetObject>()
        {
            new SetObject()
            {
                ObjectName = "LastUsedWeapon",
                DataObject = jsonString
            },
        };

        PlayFabDataAPI.SetObjects(new SetObjectsRequest()
        {
            Entity = new PlayFab.DataModels.EntityKey { Id = PlayerEntity.id, Type = PlayerEntity.type },
            Objects = dataList,
        }, (setResult) =>
        {
            RequestLastWeaponInfo();
            
        },
        (error) =>
        {
            Debug.Log(error.ErrorDetails);
        });     
    }

    public void RequestLastWeaponInfo()
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
                if (result.Objects.ContainsKey("LastUsedWeapon"))
                {
                    OnReceivedLastWeapon?.Invoke(result.Objects["LastUsedWeapon"]);
                }
                else
                {
                    OnPlayerIsNewAndHasNoData?.Invoke();
                }
                //Debug.Log(result.Objects["LastUsedWeapon"].ToJson());
            },
            (error) =>
            {                 
                Debug.Log(error);
            });
    }
}
