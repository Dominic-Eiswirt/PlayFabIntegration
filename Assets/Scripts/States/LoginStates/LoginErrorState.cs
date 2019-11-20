using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginErrorState : UIState
{
    public LoginErrorState()
    {
        referenceObj = Resources.Load("Prefabs/LoginErrorView") as GameObject;        
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

