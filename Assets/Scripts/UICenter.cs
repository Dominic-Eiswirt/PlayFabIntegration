using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


//Serves as a reference container as well as a link for creating/keeping track of current states.
//Not all states are created here however, some are created in PlayFabLogin, where the result of a request is received
public class UICenter : MonoBehaviour
{    
    public static UICenter instance;        
    [Space(5)]   
    public UIState currentState;    
    public bool lobbyCheat = false;
    public bool testInventoryState = true;
    public GameObject inventory;
    public Canvas canvas;
    public PlayFabLogin playFab;
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
        currentState = new LoginState();
        currentState.DisplayState();
    }
    private void Start()
    {  
        if(lobbyCheat)
        {             
            ChangeState(new LobbyState());
        }
        if(testInventoryState)
        {
            ChangeState(new InventoryState());
        }
    }

    public void SetCreateState()
    {
        ChangeState(new CreateAccountInitialState());        
    }

    public void SetLoginState()
    {        
        ChangeState(new LoginState());        
    }
    public void ToggleInventory()
    {
        if(currentState.GetType() != typeof(InventoryState))
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

    public void ChangeState(UIState state)
    {
        //We want to disable what the state is doing before changing to a new state so we get a clean template for the UI
        currentState?.BeforeStateChange();                     
        currentState = state;        
        currentState.DisplayState();        
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
    IEnumerator WaitForLobby()
    {
        yield return new WaitForSeconds(2f);        
        ChangeState(new LobbyState());
    }
}
