using System;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{    
    private enum ButtonEvent { None, Login, CreateAccountInitial, LobbyInventory, CreateAccount, Back }
    [SerializeField] private ButtonEvent thisButtonEvent;

    public void OnButtonClick()
    {
        switch (thisButtonEvent)
        { 
            case ButtonEvent.Login:
                UICenter.instance.SendPlayFabRequestLogin();
            break;
            case ButtonEvent.CreateAccountInitial:
                UICenter.instance.SetCreateState();
            break;
            case ButtonEvent.LobbyInventory:
                UICenter.instance.SetInventory();
            break;
            case ButtonEvent.CreateAccount:
                UICenter.instance.SendPlayFabRequestCreateUser();
            break;
            case ButtonEvent.Back:
                UICenter.instance.ChangeState(new LoginState(UICenter.instance));
            break;
            default:
            throw new Exception("A button enum was not implemented or set");
        }
    }
}
