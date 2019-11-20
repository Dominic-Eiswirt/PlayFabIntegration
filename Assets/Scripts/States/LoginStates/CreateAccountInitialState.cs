using System;
using UnityEngine;
using System.Collections.Generic;
public class CreateAccountInitialState : UIState
{    
    public CreateAccountInitialState()
    {
        referenceObj = Resources.Load("Prefabs/CreateAccountInitialView") as GameObject;
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
