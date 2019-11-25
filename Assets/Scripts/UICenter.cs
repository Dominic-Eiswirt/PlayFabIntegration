using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


//Serves as a reference container as well as a link for creating/keeping track of current states.
//Not all states are created here however, some are created in PlayFabLogin, where the result of a request is received
public class UICenter : MonoBehaviour
{
    public static UICenter instance;
    [Space(5)]
    public UIState currentState;
    public bool lobbyCheat = false;
    public bool testInventoryState = true;
    public Canvas canvas;
    public PlayFabLogin playFab;
    public CoreGameData data = new CoreGameData();

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
        currentState = new LoginState();
        currentState.DisplayState();
    }

    private void Start()
    {
        if (lobbyCheat)
        {
            ChangeState(new LobbyState());
        }
        if (testInventoryState)
        {
            ChangeState(new InventoryState());
        }
    }

    private void OnEnable()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    //Important function used for changing states.
    public void ChangeState(UIState state)
    {
        //We want to disable what the state is doing before changing to a new state so we get a clean template for the UI
        currentState?.BeforeStateChange();
        currentState = state;
        currentState.DisplayState();
    }


    #region Toggle Function
    public void ToggleInventory()
    {
        if (currentState.GetType() != typeof(InventoryState))
        {
            ChangeState(new InventoryState());
        }
        else
        {
            ChangeState(new LobbyState());
        }
    }

    public void ToggleShop()
    {
        if (currentState.GetType() != typeof(ShopState))
        {
            ChangeState(new ShopState());
        }
        else
        {
            ChangeState(new LobbyState());
        }
    }
    #endregion

    public void ToggleLeaderboard()
    {
        if (currentState.GetType() != typeof(LeaderboardState))
        {
            ChangeState(new LeaderboardState());
        }
        else
        {
            ChangeState(new LobbyState());
        }
    }



    public void SendPlayFabRequestLogin()
    {
        playFab.CreateRequestLogin();
    }

    public void SendPlayFabRequestCreateUser()
    {
        playFab.CreateRequestNewUser();
    }

    public void PlayFabUpdateEmail(Text txt)
    {
        playFab.SetEmail(txt);
    }

    public void PlayFabUpdatePassword(Text txt)
    {
        playFab.SetPassword(txt);
    }

    public void TriggerLobbyCouroutine()
    {
        StartCoroutine(WaitForLobby());
    }
    private IEnumerator WaitForLobby()
    {
        yield return new WaitForSeconds(2f);
        ChangeState(new LobbyState());
    }

    public void SetNewData(CoreGameData data)
    {
        this.data = data;
        UpdatePlayfabVirtualCurrency();


    }
    private void UpdatePlayfabVirtualCurrency()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "updateVirtualCurrency",
            FunctionParameter = new { Kills = UICenter.instance.data.score },
            GeneratePlayStreamEvent = true,

        },
        ResultCallback =>
        {
            Debug.Log("Executed script for adding virtual currency");
        }
        ,
        CloudExecuteError =>
        {
            Debug.Log("Virtual currency addition failed");
        });
    }

}
