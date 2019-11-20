using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LobbyState : UIState
{
    private GameObject lobbyStateObj;
    
    public LobbyState(UICenter center)
    {
        lobbyStateObj = Resources.Load("Prefabs/LobbyStateView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as LobbyReferences;
        //    if (myReferences != null)
        //    {
        //        break;
        //    }
        //}
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(lobbyStateObj);
        //SetAllReferences(false);
    }

    public override void DisplayState()
    {
        lobbyStateObj = GameObject.Instantiate(lobbyStateObj, canvas.transform);
        //SetAllReferences(true);
    }

    void SetAllReferences(bool condition)
    {
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {
            o.SetActive(condition);
        }
    }
}
