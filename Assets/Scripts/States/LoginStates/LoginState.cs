using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginState : UIState
{
    public GameObject loginPrefab;
    public LoginState(UICenter center) 
    {
        if(canvas == null)
        {
            canvas = center.canvas;
        }
        loginPrefab = Resources.Load("Prefabs/LoginStateView") as GameObject;
        
       // this.center = center;
       // foreach (StateReferences r in this.center.References)
       // {
       //     myReferences = r as LoginReferences;
       //     if (myReferences != null)
       //     {
       //         break;
       //     }
       // }
    }
    public override void DisplayState()
    {
        //center.SetControlReferences(myReferences, true);
        // SetAllReferences(true);
        loginPrefab = GameObject.Instantiate(loginPrefab, canvas.transform);
    }
    public override void BeforeStateChange()
    {
        //center.SetControlReferences(myReferences, false);
        // SetAllReferences(false);
        GameObject.Destroy(loginPrefab);
    }
    void SetAllReferences(bool condition)
    {        
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {
            o.SetActive(condition);
        }
    }
}

