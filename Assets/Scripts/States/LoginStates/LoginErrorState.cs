using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginErrorState : UIState
{
    string myPath = "LoginErrorView";
    public LoginErrorState()
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

