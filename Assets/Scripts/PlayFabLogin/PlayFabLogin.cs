using PlayFab;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.Json;

public class PlayFabLogin : PlayFabFunctions
{
    [SerializeField] private static string email = "";    
    public static string Email
    {
        get
        {
            return email;
        }
    }
    [SerializeField] private static string password = "";
    public static string Password
    {
        get
        {
            return Password;
        }
    }

    private RegisterPlayFabUserRequest request;
    private LoginWithEmailAddressRequest login;

    public void CreateRequestNewUser()
    {
        request = new RegisterPlayFabUserRequest()
        {
            Password = password,
            Email = email,
            RequireBothUsernameAndEmail = false,            
            TitleId = "B8FED"
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFail);
    }
    public void CreateRequestLogin()
    {
        login = new LoginWithEmailAddressRequest()
        {
            Password = password,
            Email = email,
            TitleId = "B8FED"
        };
        PlayFabClientAPI.LoginWithEmailAddress(login, OnLoginSuccess, OnLoginFail);
        
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Result is " + result);
        UICenter.instance.ChangeState(new CreateAccountSuccessState(UICenter.instance));        
    }
    void OnRegisterFail(PlayFabError error)
    {
        Debug.LogError("Error " + error);
        if(error.Error == PlayFabErrorCode.EmailAddressNotAvailable)
        {
            UICenter.instance.ChangeState(new CreateAccountAlreadyExistsState(UICenter.instance));
        }
        else 
        { 
            UICenter.instance.ChangeState(new CreateAccountErrorState(UICenter.instance));
            email = "";
            password = "";
        }
        
    }

    private void Update()
    {
        Debug.Log(email);
        Debug.Log(password);
    }
    void OnLoginSuccess(LoginResult result)
    {
        //Login part debug message
        Debug.Log("Result is " + result.LastLoginTime);
        UICenter.instance.ChangeState(new LoginSuccessState(UICenter.instance));
    }   

    void OnLoginFail(PlayFabError error)
    {
        //Login failed, could be several reasons, most likely user writing error
        Debug.Log("Error " + error);
        Debug.Log("User most likely does not exist, display error message from here");
        UICenter.instance.ChangeState(new LoginErrorState(UICenter.instance));
        email = "";
        password = "";
    }
   
   
    public override void SetPassword(Text txt)
    {        
        password = txt.text;
    }

    public override void SetEmail(Text txt)
    {
        email = txt.text;
    }
}
