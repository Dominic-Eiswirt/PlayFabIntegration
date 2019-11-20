using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LoginSuccessState : UIState
{
    GameObject loginSuccessObj;
    public LoginSuccessState(UICenter center)
    {
        loginSuccessObj = Resources.Load("Prefabs/LoginSuccessView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as LoginSuccessReferences;
        //    if (myReferences != null)
        //    {
        //        break;
        //    }
        //}
    }
    public override void DisplayState()
    {        
        //SetAllReferences(true);
        loginSuccessObj = GameObject.Instantiate(loginSuccessObj, canvas.transform);
        UICenter.instance.TriggerLobbyCouroutine();
    }
    public override void BeforeStateChange()
    {     
        GameObject.Destroy(loginSuccessObj);
        //SetAllReferences(false);
    }
    void SetAllReferences(bool condition)
    {        
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {
            o.SetActive(condition);
        }
    }
}

