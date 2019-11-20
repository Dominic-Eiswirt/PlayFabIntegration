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
    private static string email = "";    
    public static string Email
    {
        get
        {
            return email;
        }
    }
    private static string password = "";
    public static string Password
    {
        get
        {
            return password;
        }
    }

    private RegisterPlayFabUserRequest request;
    private LoginWithEmailAddressRequest login;

    public void CreateRequestNewUser()
    {
        request = new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
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
        UICenter.instance.ChangeState(new CreateAccountSuccessState());        
    }
    void OnRegisterFail(PlayFabError error)
    {   
        if(error.Error == PlayFabErrorCode.EmailAddressNotAvailable)
        {
            email = "";
            password = "";
            UICenter.instance.ChangeState(new CreateAccountAlreadyExistsState());
        }
        else if(IsEmailValid() && password.Length < 7)
        {
            email = "";
            password = "";
            UICenter.instance.ChangeState(new CreateAccountInvalidPasswordLengthState());
        }
        else
        { 
            email = "";
            password = "";
            UICenter.instance.ChangeState(new CreateAccountErrorState());
        }
        
    }

    bool IsEmailValid()
    {
        try
        {
            System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }    
    void OnLoginSuccess(LoginResult result)
    {        
        UICenter.instance.ChangeState(new LoginSuccessState());
    }   

    void OnLoginFail(PlayFabError error)
    {
        //Login failed, could be several reasons, most likely user writing error
        email = "";
        password = "";        
        Debug.Log("User most likely does not exist, display error message from here");
        UICenter.instance.ChangeState(new LoginErrorState());
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
