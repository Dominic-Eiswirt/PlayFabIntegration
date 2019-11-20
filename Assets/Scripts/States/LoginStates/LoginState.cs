using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginState : UIState
{
    string myPath = "LoginStateView";
    public LoginState()
    {
        ResetReference();
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
        ResetReference();
    }
    void ResetReference()
    {
        referenceObj = Resources.Load(resourcesPath + myPath) as GameObject;
    }
}

