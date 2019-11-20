using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginState : UIState
{    
    public LoginState() 
    {
        referenceObj = Resources.Load("Prefabs/LoginStateView") as GameObject;
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
    }
}

