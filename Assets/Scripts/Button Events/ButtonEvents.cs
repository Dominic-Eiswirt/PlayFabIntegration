using System;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{    
    private enum ButtonEvent { None, Login, CreateAccountInitial, LobbyInventory, CreateAccount, Back, Shop, PlayGame, Leaderboard }
    [SerializeField] private ButtonEvent thisButtonEvent;

    public void OnButtonClick()
    {
        switch (thisButtonEvent)
        { 
            case ButtonEvent.Login:
                UICenter.instance.SendPlayFabRequestLogin();
                break;
            case ButtonEvent.CreateAccountInitial:
                UICenter.instance.ChangeState(new CreateAccountInitialState());
                break;
            case ButtonEvent.LobbyInventory:
                UICenter.instance.ToggleInventory();
                break;
            case ButtonEvent.CreateAccount:
                UICenter.instance.SendPlayFabRequestCreateUser();
                break;
            case ButtonEvent.Back:
                UICenter.instance.ChangeState(new LoginState());
                break;            
            case ButtonEvent.Shop:
                UICenter.instance.ToggleShop();
                break;
            case ButtonEvent.PlayGame:
                CurrentWeaponLoadout.instance.AdjustPlayfabInventory();                
                break;
            case ButtonEvent.Leaderboard:
                UICenter.instance.ToggleLeaderboard();
                break;
            
            default:
            throw new Exception("A button enum was not implemented or set");
        }
    }
}
