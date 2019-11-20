using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LoginSuccessState : UIState
{    
    public LoginSuccessState()
    {
        referenceObj = Resources.Load("Prefabs/LoginSuccessView") as GameObject;
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
        UICenter.instance.TriggerLobbyCouroutine();
    }
    public override void BeforeStateChange()
    {     
        GameObject.Destroy(referenceObj);        
    }
}

