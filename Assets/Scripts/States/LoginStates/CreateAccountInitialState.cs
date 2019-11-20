using System;
using UnityEngine;
using System.Collections.Generic;
public class CreateAccountInitialState : UIState
{    
    string myPath = "CreateAccountInitialView";
    public CreateAccountInitialState()
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
