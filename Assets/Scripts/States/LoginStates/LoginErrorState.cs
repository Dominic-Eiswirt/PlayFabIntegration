using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginErrorState : UIState
{
    private string myPath = "LoginErrorView";
    public LoginErrorState()
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

