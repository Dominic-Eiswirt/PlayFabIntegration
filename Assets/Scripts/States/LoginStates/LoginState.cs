using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginState : UIState
{
    private string myPath = "LoginStateView";
    public LoginState()
    {
        ResetReference(myPath);
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
        ResetReference(myPath);
    }
}

