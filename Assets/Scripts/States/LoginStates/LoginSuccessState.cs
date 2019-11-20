using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LoginSuccessState : UIState
{
    string myPath = "LoginSuccessView";
    public LoginSuccessState()
    {
        ResetReference();
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
        UICenter.instance.TriggerLobbyCouroutine();
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

