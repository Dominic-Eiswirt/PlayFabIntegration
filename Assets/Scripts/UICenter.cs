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
    private List<StateReferences> references = new List<StateReferences>();    
    public List<StateReferences> References { get { return references; } }
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
        references.AddRange(GetComponents<StateReferences>());
        currentState = new LoginState(this);
        currentState.DisplayState();
    }
    private void Start()
    {  
        if(lobbyCheat)
        {             
            ChangeState(new LobbyState(this));
        }
        if(testInventoryState)
        {
            ChangeState(new InventoryState(this));
        }
    }

    public void SetCreateState()
    {
        ChangeState(new CreateAccountInitialState(this));        
    }

    public void SetLoginState()
    {        
        ChangeState(new LoginState(this));        
    }
    public void SetInventory()
    {
        if(currentState.GetType() != typeof(InventoryState))
        {
            Debug.Log("Changing to inventorystate");
            ChangeState(new InventoryState(this));
        }
        else
        {
            Debug.Log("Changing to lobbystate");
            ChangeState(new LobbyState(this));
        }
        //inventory.SetActive(!inventory.activeSelf);
    }
    public void ChangeState(UIState state)
    {
        //We want to disable what the state is doing before changing to a new state so we get a clean template for the UI
        currentState?.BeforeStateChange();
        currentState = state;
        currentState.DisplayState();
        Debug.Log("Current state: " + currentState.ToString());
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
        Debug.Log("in couroutine");
        yield return new WaitForSeconds(2f);
        Debug.Log("triggering");
        ChangeState(new LobbyState(this));
    }
}
