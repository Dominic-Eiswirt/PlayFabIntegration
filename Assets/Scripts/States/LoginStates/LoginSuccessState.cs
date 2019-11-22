using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LoginSuccessState : UIState
{
    private string myPath = "LoginSuccessView";
    public LoginSuccessState()
    {
        ResetReference(myPath);
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
        UICenter.instance.TriggerLobbyCouroutine();
    }
    public override void BeforeStateChange()
    {     
        GameObject.Destroy(referenceObj);
        ResetReference(myPath);
    }
}

